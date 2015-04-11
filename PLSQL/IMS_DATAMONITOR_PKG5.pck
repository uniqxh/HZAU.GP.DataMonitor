create or replace package IMS_DATAMONITOR_PKG is
  procedure chk_receipt_amount(RS out SYS_REFCURSOR,rs_count out number);
  procedure chk_scheme_execute;
  procedure execute_scheme(i_pk_id in varchar2); 
  
end IMS_DATAMONITOR_PKG;
/
create or replace package body IMS_DATAMONITOR_PKG is
  --����տ���Ϊ0
  procedure chk_receipt_amount(RS out SYS_REFCURSOR,rs_count out number) as
  begin
     open rs for
     select t.* from test2 t;
    --where t.pk_id >= to_date('2012-01-01', 'yyyy-mm-dd')
    --and t.amount <= 0;
    select count(*) into rs_count 
    from 
    ( select t.* from test2 t
    --where t.gl_date >= to_date('2012-01-01', 'yyyy-mm-dd')
    --and t.amount <= 0
    );
  end chk_receipt_amount;
  
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
    v_msg_type number;
    v_sql_type_id varchar2(50);
    v_content varchar2(200);
    v_count number;
    v_sql varchar2(1000);
    rs sys_refcursor;
    begin 
      --��������ȡ����
      select t.sql_text,t.scheme_name,t.notice_type_id,t.notice_to,t.sql_type_id
      into v_sql_text, v_scheme_name, v_notice_type_id,v_notice_to,v_sql_type_id
      from mnt_scheme t
      where t.pk_id = i_pk_id;
      
      if v_sql_type_id = 'sql' then
              --ִ��SQL��䣬�����ؽ������Ŀ
              begin
                 execute immediate 'select count(*) from ( ' || v_sql_text || ')'  into v_count;
              end;
              
      else
              -- ִ�д洢���̣������ؽ��
                execute immediate 'begin ' || v_sql_text || '(:rs,:v_count) ; end;'
                using out rs,out v_count;
      
      end if;
      
      --�ж��Ƿ����쳣���ݣ����о�֪ͨ��ϵ�ˣ�û���򲻴���
      if v_count > 0 then
        
          --����֪ͨ��ʽ
          if v_notice_type_id = 'mail' then
            v_msg_type := 1;
          elsif v_notice_type_id = 'msg' then
            v_msg_type := 0;
          elsif v_notice_type_id = 'rtx' then
            v_msg_type := 2;
          else
            v_msg_type :=-2;
          end if;
          
          v_content := '��������' || v_scheme_name || '����' || to_char(sysdate,'yyyy-mm-dd hh24:mi:ss') || '�����쳣���ݣ���' || v_notice_to ||
                                               '��ʱ�鿴����';
          
          --֪ͨ��ϵ��
          v_sql := 'insert into fms.fms_fpm_msg(MSG_ID,MSG_NAME,SEND_TO,FROM_APP,SUBJECT,CONTENT,PRIORITY,MSG_TYPE,CREATION_DATE) 
          values(fms.fms_fpm_msg_s.nextval,''' 
          ||v_scheme_name || ''',''' || v_notice_to || ''',''IMSϵͳ'','''|| v_scheme_name || ''',''' || v_content || ''',1,' || v_msg_type ||',sysdate)';
          dbms_output.put_line(v_sql);
          begin
              execute immediate v_sql;
          end;
      end if;
    end execute_scheme;

end IMS_DATAMONITOR_PKG;
/
