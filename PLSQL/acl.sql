-- ���� ACL
BEGIN
 DBMS_NETWORK_ACL_ADMIN.CREATE_ACL (
  acl          => 'email_server_permissions.xml', 
  description  => 'Enables network permissions for the e-mail server',
  principal    => 'C##TEST',
  is_grant     => TRUE, 
  privilege    => 'connect');
END;
/

-- ���ʼ��������
BEGIN
 DBMS_NETWORK_ACL_ADMIN.ASSIGN_ACL (
  acl         => 'email_server_permissions.xml',
  host        => 'smtp.163.com', 
  lower_port  => 25); 
END;
/

-- ���� email_user �û��ʻ������Ĵ洢���̱��������ʼ������������ʼ�
