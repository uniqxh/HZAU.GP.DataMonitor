using System;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using HZAU.GP.DataMonitor.Entity.Database;
using HZAU.GP.DataMonitor.Entity;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class GetDataMonitorBiz : IGetDataMonitorBiz
    {
        //private IRepository repository;
        private IExecuteSchemeProvider executeSchemeProvider;
        private IConnection connection;
        #region Max export excel row count
        //private static string maxRowCount = ConfigurationManager.AppSettings["MaxExptExcelCount"] != null ? ConfigurationManager.AppSettings["MaxExptExcelCount"] : "50000";
        #endregion

        public GetDataMonitorBiz(IExecuteSchemeProvider executeSchemeProvider, IConnection connection)
        {
          //  this.repository = repository;
            this.executeSchemeProvider = executeSchemeProvider;
            this.connection = connection;
        }

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
        //    }
        //}

        public SchemeEntity GetSchemeEntityById(int pkId)
        {
            if (pkId == null)
            {
                return null;
            }
            else
            {
                Sql sql = new Sql();
                sql.Where(Fields.PkId).EqualTo(pkId);
                var resultList = this.connection.Select<SchemeEntity>(sql.GetSql());
                if (resultList.Count > 0) return resultList[0];
                else return null;
            }
        }

        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public SchemeSearchResult SearchScheme(SchemeSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }
            string sql = criteria.GetSql();
            var resultList = this.connection.Select<SchemeEntity>(sql);
            SchemeSearchResult result = new SchemeSearchResult();
            result.SchemeList = resultList;
            result.SchemeSearchCriteria = criteria;
            return result;
        }
        public string ExportSchemeToExcel(int pkId)
        {
            var entity = this.GetSchemeEntityById(pkId);
            if (entity == null || string.IsNullOrEmpty(entity.SCHEME_ID))
            {
                throw new Exception("找不到对应方案信息");
            }
            var provider = this.executeSchemeProvider.GetSchemeExecuteType(entity.SQL_TYPE_ID, connection);
            return provider.ExportExecuteResultToExcel(entity);
        }
    }
}
