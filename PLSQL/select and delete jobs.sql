--delete jobs
begin
  dbms_job.BROKEN(1, true);
  sys.dbms_job.remove(1);
  exception when others then 
    dbms_output.put_line(dbms_utility.format_error_stack);
       dbms_output.put_line(dbms_utility.format_call_stack);
end;
--select jobs
select job, log_user from dba_jobs;
