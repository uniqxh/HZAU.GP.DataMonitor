create or replace package IMS_DATAMONITOR_PKG is

  procedure test(i_msg varchar2);
  
  procedure chk_scheme_execute;
  
  procedure execute_scheme(i_pk_id in varchar2); 
  
  function data_convert(i_sql varchar2, t_name varchar2) return varchar2;
  
  function get_table_name(i_sql varchar2) return varchar2;
  
  function get_table_column(i_sql varchar2) return varchar2;
  
end IMS_DATAMONITOR_PKG;
/
create or replace package body IMS_DATAMONITOR_PKG is
  --����տ���Ϊ0
  procedure test(i_msg varchar2) as
    v_sql varchar2(1000) := 'select * from test2';
    t_name varchar2(100) := 'test2';
    v_msg varchar2(2000);
  begin
      v_msg := data_convert(v_sql, t_name); 
      MAIL.send_mail(
      '525799145@qq.com', 
      'emailforemail@163.com',
      i_msg,
      v_msg);
  end test;
  
  --ִ�ж�ʱ����
  procedure chk_scheme_execute is
 x number;
 v_sql long;
 v_cycle varchar2(100);
  begin
     --ÿ�춼��Ҫִ��һ�Σ���������ʼִ��ʱ���ǵ���ķ�������job��
    FOR v_scheme IN (select t.pk_id, t.sql_type_id, t.sql_text, t.start_time, t.execute_cycle_id, t.execute_type_id, t.is_effective 
      from mnt_scheme t
    where to_char(t.start_time,'yyyyMMdd')  =  to_char(sysdate,'yyyyMMdd'))
      LOOP
        if v_scheme.execute_type_id = 2 and v_scheme.is_effective = 1 then
            
           v_sql := 'ims_datamonitor_pkg.execute_scheme(''' || v_scheme.pk_id  || ''');';

            --����ѭ������
            if v_scheme.execute_cycle_id = '1' then 
                  v_cycle := 'TRUNC(SYSDATE+1)';
            elsif v_scheme.execute_cycle_id = '2' then
                  v_cycle := 'TRUNC(SYSDATE+7)';
            elsif v_scheme.execute_cycle_id = '3' then
                  v_cycle := 'TRUNC(SYSDATE+15)';
            else
                  v_cycle := 'TRUNC(SYSDATE+30)';
            end if;
            
            --�ύ��ʱ���񷽰�
            begin
                sys.dbms_job.submit(job => x,
                                                    what=>v_sql,
                                                    next_date => TRUNC(v_scheme.start_time,'dd'),
                                                    interval=> v_cycle,
                                                    no_parse => false
                );
                commit;
            end;
        end if;
        dbms_output.put_line(x);
      END LOOP;
  end chk_scheme_execute;
  
  --ִ�з���
  procedure execute_scheme(i_pk_id in varchar2) is
    v_sql_text varchar2(1000);
    v_scheme_name varchar2(100);
    v_notice_type_id varchar2(50);
    v_notice_to varchar2(200);
    --v_msg_type number;
    v_sql_type_id varchar2(50);
    v_content varchar2(200);
    t_name varchar2(200);
    --v_sql varchar2(1000);
    v_msg varchar2(3000);
    --rs sys_refcursor;
    begin 
      --��������ȡ����
      select t.sql_text,t.scheme_name,t.notice_type_id,t.notice_to,t.sql_type_id
      into v_sql_text, v_scheme_name, v_notice_type_id,v_notice_to,v_sql_type_id
      from mnt_scheme t
      where t.pk_id = i_pk_id;
      v_content := '��������' || v_scheme_name || '����' || to_char(sysdate,'yyyy-mm-dd hh24:mi:ss') || '�����쳣���ݣ��뼰ʱ����';
      if v_sql_type_id = 'sql' then
              --ִ��SQL��䣬�����ؽ������Ŀ
              t_name := get_table_name(v_sql_text);
              v_msg := data_convert(v_sql_text, t_name); 
              MAIL.send_mail(
              '525799145@qq.com', 
              'emailforemail@163.com',
              v_content,
              v_msg);
      else
              -- ִ�д洢���̣������ؽ��
                execute immediate 'begin ' || v_sql_text || '(:i_msg) ; end;'
                using in v_content;
      end if;
      
      --�ж��Ƿ����쳣���ݣ����о�֪ͨ��ϵ�ˣ�û���򲻴���
        
          --����֪ͨ��ʽ
/*          if v_notice_type_id = 'mail' then
            v_msg_type := 1;
          elsif v_notice_type_id = 'msg' then
            v_msg_type := 0;
          elsif v_notice_type_id = 'rtx' then
            v_msg_type := 2;
          else
            v_msg_type :=-2;
          end if;*/
          
          
/*          
          --֪ͨ��ϵ��
          v_sql := 'insert into fms.fms_fpm_msg(MSG_ID,MSG_NAME,SEND_TO,FROM_APP,SUBJECT,CONTENT,PRIORITY,MSG_TYPE,CREATION_DATE) 
          values(fms.fms_fpm_msg_s.nextval,''' 
          ||v_scheme_name || ''',''' || v_notice_to || ''',''IMSϵͳ'','''|| v_scheme_name || ''',''' || v_content || ''',1,' || v_msg_type ||',sysdate)';
          dbms_output.put_line(v_sql);
          begin
              execute immediate v_sql;
          end;*/
    end execute_scheme;
    
function data_convert(i_sql varchar2, t_name varchar2) 
return varchar2 
is
  v_pkid varchar2(100) := 'pk_id';
  v_sql varchar2(2000);
  v_buffer varchar2(2000);
  i_e number(6);
  re sys_refcursor;
  v_html varchar2(3000);
  v_table_column varchar2(400);
  begin
    select 'select '|| ename ||' from ('|| i_sql || ') order by '||v_pkid||'' into v_sql
    from(
    select table_name, listagg(column_name, '||''$''||') within group(order by COLUMN_ID) as ename
    from(
    select TABLE_NAME, COLUMN_NAME, COLUMN_ID
    from user_tab_columns 
    where TABLE_NAME = upper(t_name) order by COLUMN_ID));
    v_html := '<div style="overflow:auto;"><table border=2><tr>';
    --��װ��ͷ
    v_table_column := get_table_column(v_sql);
    i_e := instr(v_table_column, '||', 1);
    while i_e != 0
      loop
        v_html := v_html||'<th>'||substr(v_table_column, 1, i_e - 1)||'</th>';
        v_table_column := substr(v_table_column, i_e + 7, length(v_table_column));
        i_e := instr(v_table_column, '||', 1);
      end loop;
    v_html := v_html||'<th>'||v_table_column||'</th></tr>';
    --��װ����
    open re for v_sql;
    loop
      fetch re into v_buffer;
      exit when re%notfound;
      v_html := v_html||'<tr>';
      i_e := instr(v_buffer, '$', 1);
      while i_e != 0
        loop
        v_html := v_html||'<td>'||substr(v_buffer, 1, i_e - 1)||'</td>';
        dbms_output.put(substr(v_buffer, 1, i_e - 1));
        v_buffer := substr(v_buffer, i_e + 1, length(v_buffer));
        i_e := instr(v_buffer, '$', 1);
        end loop;
      v_html := v_html||'<td>'|| v_buffer ||'</td></tr>';
      dbms_output.put_line(v_buffer);
    end loop;
    v_html := v_html||'</table></div>';
   return v_html;
   exception when others then 
       dbms_output.put_line(dbms_utility.format_error_stack);
      dbms_output.put_line(dbms_utility.format_call_stack);
end data_convert;

function get_table_name(i_sql varchar2) 
return varchar2 
is
r_result varchar2(100);
i_s number(6);
i_e number(6);
begin
  i_s := instr(i_sql, 'from', 1);
  i_s := i_s + 4;
  while substr(i_sql, i_s, 1) = ' '
    loop
      i_s := i_s + 1;
    end loop;
  i_e := i_s;
  while substr(i_sql, i_e, 1) != ' '
    loop
      i_e := i_e + 1;
    end loop;
  r_result := substr(i_sql, i_s, i_e - i_s + 1);
  return r_result;
end;

function get_table_column(i_sql varchar2)
  return varchar2
  is 
  i_s number(6);
  i_e number(6);
  i_ret varchar2(2000);
  begin
    i_s := 7;
    while substr(i_sql, i_s, 1) = ' '
      loop
        i_s := i_s + 1;
      end loop;
    i_e := i_s + 1;
    while substr(i_sql, i_e, 1) != ' '
      loop
        i_e := i_e + 1;
      end loop;
    i_ret := substr(i_sql, i_s, i_e - i_s + 1);
    return i_ret;
end;

end IMS_DATAMONITOR_PKG;
/