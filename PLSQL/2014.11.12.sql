 select * from user_tables t where t.TABLE_NAME = 'ims3_file_info'
select distinct t.TABLE_NAME from user_tab_columns t where t.TABLE_NAME like 'IMS3_RECEIPT'
select * from user_tab_columns t where upper(t.TABLE_NAME) = upper('ims3_file_info')

select * from dba_tab_columns t where upper(t.TABLE_NAME) = upper('ims3_file_info')


select wmsys.wm_concat(COLUMN_NAME) 
from dba_tab_columns a 
where upper(a.TABLE_NAME) = upper('ims3_file_info')

select 'select sum(ora_hash('||enames||', power(2,30),0)) from '||owner||'.'||table_name||';'
from
(

select owner, table_name, replace(wmsys.wm_concat(column_name),',','||') as enames
from ( select owner, table_name, column_name from dba_tab_columns where upper(table_name) = upper('ims3_file_info') )
group by owner, table_name
);
select PK_ID, ora_hash(PK_ID||CREATE_BY_NAME||CREATE_BY||CREATE_DATE||WEBSITE_URL||STATUS||UP_FILE_NAME||SOURCE_FILE_NAME||DESCRIPTION||MODULE_BUSINESS_ID||MODULE_NAME||STAMP, power(2,30),0) from IMS3.IMS3_FILE_INFO order by PK_ID;

select distinct(ora_hash(PK_ID||CREATE_BY_NAME||CREATE_BY||CREATE_DATE||WEBSITE_URL||STATUS||UP_FILE_NAME||SOURCE_FILE_NAME||DESCRIPTION||MODULE_BUSINESS_ID||MODULE_NAME||STAMP, power(2,30),0)) from IMS3.IMS3_FILE_INFO;
select pk_id from ims3_file_info where pk_id = 1

select¡¡* from user_tab_columns
