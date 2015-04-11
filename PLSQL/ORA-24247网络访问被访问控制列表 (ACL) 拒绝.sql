----solve "ORA-24247: ������ʱ����ʿ����б� (ACL) �ܾ�"
BEGIN
 DBMS_NETWORK_ACL_ADMIN.CREATE_ACL (
  acl          => 'email_server_permissions.xml',
  description  => 'Enables network permissions for the e-mail server',
  principal    => 'C##TEST',
  is_grant     => TRUE,
  privilege    => 'connect');
END;
/

BEGIN
DBMS_NETWORK_ACL_ADMIN.assign_acl (
    acl         => 'email_server_permissions.xml',
    host        => 'smtp.qq.com', --SMTP��������ַ
    lower_port  => 25,
    upper_port  => NULL);
  COMMIT;
END;
/
 --drop
BEGIN
  DBMS_NETWORK_ACL_ADMIN.drop_acl(acl => 'email_server_permissions.xml');
  COMMIT;
END;
/
--��ѯ
  SELECT host, lower_port, upper_port, acl FROM dba_network_acls;
SELECT acl,
       principal,
       privilege,
       is_grant,
       TO_CHAR(start_date, 'DD-MON-YYYY') AS start_date,
       TO_CHAR(end_date, 'DD-MON-YYYY') AS end_date
  FROM dba_network_acl_privileges;
