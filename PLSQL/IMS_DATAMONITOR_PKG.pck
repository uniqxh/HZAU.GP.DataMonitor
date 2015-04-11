create or replace package IMS_DATAMONITOR_PKG is
  procedure chk_receipt_amount(RS out SYS_REFCURSOR);
  procedure chk_scheme_execute;
  procedure execute_scheme(i_pk_id in varchar2);
end IMS_DATAMONITOR_PKG;
/
create or replace package body IMS_DATAMONITOR_PKG is
  --检查收款金额为0
  procedure chk_receipt_amount(RS out SYS_REFCURSOR) as
  begin
    open rs for
    select t.* from ims3_receipt t
    where t.gl_date >= to_date('2012-01-01', 'yyyy-mm-dd')
    and t.amount <= 0;
  end chk_receipt_amount;
  
  --执行定时方案
  procedure chk_scheme_execute is
 x number;
 v_sql long;
  begin

    FOR ss IN (select t.pk_id, t.sql_type_id, t.sql_text, t.start_time, t.execute_cycle_id, t.execute_type_id, t.is_effective 
      from ims3_mnt_scheme t
    where to_char(t.start_time,'yyyyMMdd')  <  to_char(sysdate,'yyyyMMdd'))
      LOOP
        if ss.execute_type_id = 2 and ss.is_effective = 1 then
            if ss.sql_type_id = 'sql' then
                 v_sql := 'ims_datamonitor_pkg.execute_scheme(''' || ss.pk_id  || ''');';
            else
                  v_sql := ss.sql_text || ';';
            end if;
            
            begin
                sys.dbms_job.submit(job => x,
                                                    what=>v_sql,
                                                    next_date => TRUNC(ss.start_time,'dd'),
                                                    interval=> 'TRUNC(SYSDATE+1)',
                                                    no_parse => false
                );
                commit;
            end;
        end if;
        dbms_output.put_line(x);
      END LOOP;
  end chk_scheme_execute;
  
  procedure execute_scheme(i_pk_id in varchar2) is
    v_sql_text varchar2(1000);
    v_scheme_name varchar2(100);
    v_notice_type_id varchar2(50);
    v_notice_to varchar2(200);
    v_msg_type number;
    v_count number;
    v_sql varchar2(1000);
    begin 
      select t.sql_text,t.scheme_name,t.notice_type_id,t.notice_to
      into v_sql_text, v_scheme_name, v_notice_type_id,v_notice_to
      from ims3_mnt_scheme t
      where t.pk_id = i_pk_id;
      execute immediate 'select count(*) from ( ' || v_sql_text || ')'  into v_count;
      
      dbms_output.put_line(v_count);
      
      if v_count > 0 then
          if v_notice_type_id = 'mail' then
            v_msg_type := 1;
          else if v_notice_type_id = 'msg' then
            v_msg_type := 0;
          else if v_notice_type_id = 'rtx' then
            v_msg_type := 2;
          else
            v_msg_type :=-2;
          end if;
          v_sql := 'insert into fms_fpm_msg(MSG_NAME,SEND_TO,CONTENT,MSG_TYPE) values(' 
          ||v_scheme_name || ',' || v_notice_to || ',' || '数据发生异常' || v_msg_type ||')';
          dbms_output.put_line(v_sql);
      end if;
    end execute_scheme;

end IMS_DATAMONITOR_PKG;
/
