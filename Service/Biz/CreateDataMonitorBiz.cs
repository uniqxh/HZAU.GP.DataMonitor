using System;
using System.Collections.Generic;
using HZAU.GP.DataMonitor.Entity;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using System.Data;
using System.Data.OracleClient;
using System.Text;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class CreateDataMonitorBiz:ICreateDataMonitorBiz
    {
        //private IRepository repository;
        private IGetDataMonitorBiz getSchemeBiz;
        private IConnection connection;
        //private SubmitAction currentAction;
        //public SubmitAction CurrentAction
        //{
        //    get
        //    {
        //        return this.currentAction;
        //    }
        //    set
        //    {
        //        this.currentAction = value;
        //        this.getSchemeBiz.CurrentAction = value;
        //    }
        //}
        public CreateDataMonitorBiz(IGetDataMonitorBiz getSchemeBiz, IConnection connection)
        {
         //   this.repository = repository;
            this.getSchemeBiz = getSchemeBiz;
            this.connection = connection;
        }
        public bool InsertScheme(SchemeEntity SchemeEntity)
        {
            try
            {
                SchemeEntity.STAMP = DateTime.Now;
                SchemeEntity.SCHEME_ID = this.GetSN("SHM");
                SchemeEntity.START_TIME = SchemeEntity.START_TIME ?? DateTime.Now;
                SchemeEntity.IS_EFFECTIVE = 1M;
                SchemeEntity.STATUS = "生效";
                SchemeEntity.CREATE_BY = 0;
                SchemeEntity.CREATE_BY_NAME = "system";
                SchemeEntity.CREATE_DATE = DateTime.Now;
                SchemeEntity.SQL_TEXT = DataProcess(SchemeEntity.SQL_TEXT);
                SchemeEntity.REMARK = DataProcess(SchemeEntity.REMARK);
                this.connection.Insert<SchemeEntity>(SchemeEntity);
                this.connection.Commit();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string GetSN(string name)
        {
            return string.Format("{0}{1}", name, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        /// <summary>
        /// 插入sql语句中’字符进行转义
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string DataProcess(string sql)
        {
            StringBuilder sb = new StringBuilder();
            int i = sql.IndexOf("'", 0);
            if(i<0)
            {
                return sql;
            }
            while(i >= 0)
            {
                sb.Append(sql.Substring(0, i + 1));
                sb.Append("'");
                sql = sql.Substring(i + 1, sql.Length - 1 - i);
                i = sql.IndexOf("'", 0);
            }
            sb.Append(sql);
            return sb.ToString();
        }
    }
}
