select  replace(wmsys.wm_concat(column_name), ',', '||') as enames 
from (
select table_name, column_name
from user_tab_columns
where table_name = upper('ims3_file_info') and column_name not in ('PK_ID');
)
order by table_name

select PK_ID||STAMP||MODULE_NAME||MODULE_BUSINESS_ID||DESCRIPTION||SOURCE_FILE_NAME||UP_FILE_NAME||STATUS||WEBSITE_URL||CREATE_DATE||CREATE_BY||CREATE_BY_NAME
from ims3_file_info

create table XH_TEST1(PK_ID number primary key, T_NAME varchar2(100), T_AGE number)
create table XH_TEST2(PK_ID number primary key, T_NAME varchar2(100), T_AGE number)
drop table xh_test2
select * from XH_TEST1;
select * from XH_TEST2;
insert into XH_TEST1 values(6, 'test', 100);
commit
select PK_ID, ora_hash(PK_ID||T_AGE||T_NAME, power(2,30), 0) from XH_TEST1 order by PK_ID;
select PK_ID, ora_hash(PK_ID||T_AGE||T_NAME, power(2,30), 0) from XH_TEST2 order by PK_ID;
grant select,insert,update,alter on XH_TEST1 to ims3,ims3app
insert into XH_TEST2 values(5, 'test', 100);
commit;
delete from XH_TEST1;
commit;
delete from XH_TEST2
commit;
