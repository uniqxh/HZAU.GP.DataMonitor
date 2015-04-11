create or replace package IMS_COMPARE_DB is
       procedure cmp_ds(tabname1 in varchar2, tabname2 in varchar2);
       procedure convert_to_hash(i_tabname in varchar2, rs out sys_refcursor);
end IMS_COMPARE_DB;
/
--前提条件必须2个数据源的表结构相同
create or replace package body IMS_COMPARE_DB is
       --校验2个数据源
       procedure cmp_ds(tabname1 in varchar2, tabname2 in varchar2)
       is
       rs1 sys_refcursor;
       rs2 sys_refcursor;
       pkid1 number;
       pkid2 number;
       hash1 number;
       hash2 number;
       cnt number;
       begin 
         ims_compare_db.convert_to_hash(tabname1,rs1);
         ims_compare_db.convert_to_hash(tabname2,rs2);
         cnt:=0;
         loop
           fetch rs1 into pkid1, hash1;
           fetch rs2 into pkid2, hash2;
           exit when rs1%notfound and rs2%notfound;
           if pkid1 != pkid2 and hash1 != hash2
             then 
               cnt := cnt + 1;
               dbms_output.put_line('不相同');
           end if;
         end loop;
         dbms_output.put_line('不相同总数：'||cnt);
       end cmp_ds;
       
       --将数据源每行数据转化为hash值
       procedure convert_to_hash(i_tabname in varchar2, rs out sys_refcursor)
       is
       i_sql varchar2(2000);
       begin 
         select 'select PK_ID, ora_hash('||enames||', power(2,30), 0) from '||table_name||' order by PK_ID' into i_sql
         from (
           select table_name, replace(wmsys.wm_concat(column_name), ',', '||') as enames
           from (
             select table_name, column_name
             from user_tab_columns
             where table_name = upper(i_tabname)
             )
           group by table_name);
           
         dbms_output.put_line(i_sql);
         open rs for i_sql;
      end convert_to_hash;
       
end IMS_COMPARE_DB;
/
