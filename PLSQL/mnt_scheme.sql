-- Create table
create table IMS3_MNT_SCHEME
(
  pk_id            NUMBER not null,
  stamp            TIMESTAMP(6),
  scheme_id        VARCHAR2(50) not null,
  scheme_name      VARCHAR2(100) not null,
  module_id        VARCHAR2(200) not null,
  sql_type_id      VARCHAR2(50) not null,
  sql_text         VARCHAR2(1000) not null,
  execute_type_id  VARCHAR2(50) not null,
  notice_type_id   VARCHAR2(50) not null,
  notice_to        VARCHAR2(200),
  execute_cycle_id VARCHAR2(50),
  start_time       DATE,
  is_effective     NUMBER default (1),
  status           VARCHAR2(50),
  remark           VARCHAR2(200),
  create_date      DATE not null,
  create_by        NUMBER not null,
  create_by_name   VARCHAR2(50),
  update_date      DATE,
  update_by        NUMBER,
  update_by_name   VARCHAR2(50)
)
tablespace FMS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table IMS3_MNT_SCHEME
  add primary key (PK_ID)
  using index 
  tablespace FMS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Grant/Revoke object privileges 
grant select, insert, update, delete on IMS3_MNT_SCHEME to IMS3APP;
grant insert, update, delete on IMS3_MNT_SCHEME to IMS3_DML_ROLE;
grant select on IMS3_MNT_SCHEME to IMS3_QUERY_ROLE;