create or replace package IMS_COMPARE_DB is
       procedure cmp_ds(tabname1 in varchar2, tabname2 in varchar2, key in varchar2, parm in varchar2);
       procedure convert_to_hash(i_tabname in varchar2, i_key in varchar2, i_parm in varchar2, rs out sys_refcursor);
end IMS_COMPARE_DB;
/
create or replace package body IMS_COMPARE_DB is
       --校验2个数据源
       --tabname1：数据源1
       --tabname2：数据源2
       --key：数据源主键,如果是组合键，则以逗号','分隔
       --parm：如果有多个字段以逗号','分隔
       procedure cmp_ds(tabname1 in varchar2, tabname2 in varchar2, key in varchar2, parm in varchar2)
       is
       out_str1 varchar2(100);
       out_str2 varchar2(100);
       out_str3 varchar2(100);
       out_str4 varchar2(100);
       rs1 sys_refcursor;
       rs2 sys_refcursor;
       key1 varchar2(200);
       key2 varchar2(200);
       hash1 number;
       hash2 number;

       cnt_match number;
       cnt_nomatch number;
       cnt_tab1 number;
       cnt_tab2 number;
       begin 
         --数据初始化
         cnt_match:=0;
         cnt_nomatch:=0;
         cnt_tab1:=0;
         cnt_tab2:=0;
         out_str1 := tabname1||'中有而'||tabname2||'中没有的行：';
         out_str2 := tabname2||'中有而'||tabname1||'中没有的行：';
         out_str3 := tabname1||'和'||tabname2||'不匹配的行：';
         out_str4 := tabname1||'和'||tabname2||'匹配的行：';
         
         ims_compare_db.convert_to_hash(upper(tabname1),upper(key),upper(parm),rs1);
         ims_compare_db.convert_to_hash(upper(tabname2),upper(key),upper(parm),rs2);
         fetch rs1 into key1, hash1;
         fetch rs2 into key2, hash2;
         dbms_output.put_line('');
         if rs1%rowcount = 0 and rs2%rowcount = 0
           then
             dbms_output.put_line('数据源都为空');
         elsif rs1%rowcount = 0
           then 
             dbms_output.put_line(tabname1||'数据源为空');
             loop
               exit when rs2%notfound;
               cnt_tab2 := cnt_tab2 + 1;
               dbms_output.put_line(out_str2||key2);
               fetch rs2 into key2, hash2;
             end loop;
         elsif rs2%rowcount = 0
           then 
             dbms_output.put_line(tabname2||'数据源为空');
             loop
               exit when rs1%notfound;
               cnt_tab1 := cnt_tab1 + 1;
               dbms_output.put_line(out_str1||key1);
               fetch rs1 into key1, hash1;
             end loop;
         else
           
           loop
             
             if key1 = key2
               then
                 if hash1 != hash2
                   then
                     cnt_nomatch := cnt_nomatch + 1;
                     dbms_output.put_line(out_str3||key1);
                 else 
                   cnt_match := cnt_match + 1;
                   dbms_output.put_line(out_str4||key1);
                 end if;
                 fetch rs1 into key1, hash1;
                 fetch rs2 into key2, hash2;
             elsif key1 > key2
               then
                 cnt_tab2 := cnt_tab2 + 1;
                 dbms_output.put_line(out_str2||key2);
                 fetch rs2 into key2, hash2;
             elsif key1 < key2
               then
                 cnt_tab1 := cnt_tab1 + 1;
                 dbms_output.put_line(out_str1||key1);
                 fetch rs1 into key1, hash1;
             end if;
             exit when rs1%notfound and rs2%notfound;
             if rs1%notfound
               then
                 loop
                   exit when rs2%notfound;
                   cnt_tab2 := cnt_tab2 + 1;
                   dbms_output.put_line(out_str2||key2);
                   fetch rs2 into key2, hash2;
                 end loop;
                 exit;
             elsif rs2%notfound
               then 
                 loop
                   exit when rs1%notfound;
                   cnt_tab1 := cnt_tab1 + 1;
                   dbms_output.put_line(out_str1||key1);
                   fetch rs1 into key1, hash1;
                 end loop;
                 exit;
             end if;
           end loop;
         end if;
         dbms_output.put_line('');
         dbms_output.put_line(tabname1||'和'||tabname2||'完全匹配的总数：'||cnt_match);
         dbms_output.put_line(tabname1||'和'||tabname2||'不匹配的总数：'||cnt_nomatch);
         dbms_output.put_line(tabname1||'在'||tabname2||'找不到对应行的总数：'||cnt_tab1);
         dbms_output.put_line(tabname2||'在'||tabname1||'找不到对应行的总数：'||cnt_tab2);
       end cmp_ds;
       
       --将数据源每行数据转化为hash值
       procedure convert_to_hash(i_tabname in varchar2, i_key in varchar2, i_parm in varchar2, rs out sys_refcursor)
       is
       i_sql varchar2(2000);
       i_start number;
       i_end number;
       i_len number;
       i_str varchar2(100);
       i_newkey varchar2(200);
       begin 
         i_newkey := replace(upper(i_key),',','||''-''||');
         select 'select '||i_newkey||', ora_hash('||enames||', power(2,30), 0) from '||table_name||' order by '||i_key||'' into i_sql
         from (
           select table_name, listagg(column_name,'||') within group(order by column_name) as enames
           from (
             select table_name, column_name
             from user_tab_columns
             where table_name = upper(i_tabname) order by column_name
             )
           group by table_name);
         i_start:=1;
         i_end:=1;
         --i_parm := trim(i_parm);
         i_len := length(i_parm);
         if i_len > 0
           then
             loop
               i_end := INSTR(i_parm,',',i_start,1);
               if i_end = 0
                 then 
                   i_str := trim(substr(i_parm, i_start, i_len));
                   i_sql := replace(i_sql, '||'||i_str);
                   exit;
               else
                 i_str := trim(substr(i_parm, i_start, i_end-1));
                 i_sql := replace(i_sql, '||'||i_str);
                 i_start := i_end + 1;
               end if;
             end loop;
         end if;
           
         dbms_output.put_line(i_sql);
         open rs for i_sql;
      end convert_to_hash;
       
end IMS_COMPARE_DB;
/
