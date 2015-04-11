-- 创建 ACL
BEGIN
 DBMS_NETWORK_ACL_ADMIN.CREATE_ACL (
  acl          => 'email_server_permissions.xml', 
  description  => 'Enables network permissions for the e-mail server',
  principal    => 'C##TEST',
  is_grant     => TRUE, 
  privilege    => 'connect');
END;
/

-- 与邮件服务关联
BEGIN
 DBMS_NETWORK_ACL_ADMIN.ASSIGN_ACL (
  acl         => 'email_server_permissions.xml',
  host        => 'smtp.163.com', 
  lower_port  => 25); 
END;
/

-- 这样 email_user 用户帐户创建的存储过程便可以向此邮件服务器发送邮件
