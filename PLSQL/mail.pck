create or replace package mail is

  -- Author  : XINHUA
  -- Created : 2015-03-31 18:46:50
  -- Purpose : send mail
  /*
  ** i_toMail    : 发送带附件的邮件
  ** i_fromMail  : 发送者邮箱
  ** i_subject   : 接收者邮箱
  ** i_subject   : 主题
  ** i_emailBody : 内容
  ** i_attach    : 附件
  */
  procedure send_mail(
    i_toMail varchar2, 
    i_fromMail varchar2,
    i_subject varchar2,
    i_emailBody varchar2,
    i_attach varchar2 DEFAULT NULL);
  
  

end mail;
/
create or replace package body mail is
  /*
  ** i_toMail    : 发送带附件的邮件
  ** i_fromMail  : 发送者邮箱
  ** i_subject   : 接收者邮箱
  ** i_subject   : 主题
  ** i_emailBody : 内容
  ** i_attach    : 附件
  */
  procedure send_mail(
    
    i_toMail varchar2, 
    i_fromMail varchar2,
    i_subject varchar2,
    i_emailBody varchar2,
    i_attach varchar2 DEFAULT NULL) 
    is
    v_smtphost varchar2(30) := 'smtp.163.com'; --smtp server address
    v_smtpport number(5) := 25; --smtp server port
    v_user varchar2(30) := 'emailforemail@163.com'; --smtp user name
    v_pwd varchar2(20) := '525799145'; --smtp user password
    v_con utl_smtp.connection;
    v_msg varchar2(32767);
    v_buffer varchar2(32767);
    crlf         VARCHAR2(2)  := chr(13)||chr(10);
    F utl_file.file_type;
    begin
    v_con := utl_smtp.open_connection(v_smtphost, v_smtpport);
    utl_smtp.ehlo(v_con, v_smtphost);
    
    utl_smtp.command(v_con, 'AUTH LOGIN');
    utl_smtp.command(v_con, utl_raw.cast_to_varchar2(
    utl_encode.base64_encode(utl_raw.cast_to_raw(v_user))));
    utl_smtp.command(v_con, utl_raw.cast_to_varchar2(
    utl_encode.base64_encode(utl_raw.cast_to_raw(v_pwd))));  
    
    utl_smtp.mail(v_con, i_fromMail);
    utl_smtp.rcpt(v_con, i_toMail);
    
    v_msg := 
    'Date: '  || to_char(sysdate,'yyyy-mm-dd hh24:mi:ss') || crlf || 
    'From: 数据监控系统<'  || i_fromMail || '>' || crlf || 
    'To: '    || i_toMail   || crlf || 
    'Subject: '|| i_subject || crlf ||
    'MIME-Version: 1.0'|| crlf ||	-- Use MIME mail standard
    'Content-Type: multipart/mixed;'|| crlf ||
    ' boundary="-----SECBOUND"'|| crlf ||
    crlf ||
    
    '-------SECBOUND'|| crlf ||
    'Content-Type: text/plain;'|| crlf ||
    'Content-Transfer_Encoding: 7bit'|| crlf ||
    crlf || i_emailBody || crlf || crlf;	-- Message body
    
    if i_attach is not NULL then  -- 判断有无附件
      v_msg := v_msg || 
      '-------SECBOUND'|| crlf ||
      'Content-Type: text/plain;'|| crlf ||
      ' name="'|| i_attach ||'"'|| crlf ||
      'Content-Transfer_Encoding: 8bit'|| crlf ||
      'Content-Disposition: attachment;'|| crlf ||
      ' filename="'|| i_attach ||'"'|| crlf ||
      crlf;
      F := utl_file.fopen('MY_DIR', i_attach, 'r');
      begin
      loop
        utl_file.get_line(F, v_buffer);
        v_msg := v_msg || v_buffer || crlf;
      end loop;
      utl_file.fclose(F);
      exception when NO_DATA_FOUND then
        utl_file.fclose(F);
      end;
    end if;
    
    v_msg := v_msg || crlf ||
    '-------SECBOUND--';			-- End MIME mail
    
    utl_smtp.open_data(v_con);
    utl_smtp.write_raw_data(v_con, utl_raw.cast_to_raw(v_msg));
    utl_smtp.close_data(v_con);
    utl_smtp.quit(v_con);
    exception
      when others then
      dbms_output.put_line(dbms_utility.format_error_stack);
      dbms_output.put_line(dbms_utility.format_call_stack);
      utl_smtp.quit(v_con);          
end send_mail;

end mail;
/
