using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HZAU.GP.DataMonitor.Service.Contract;
using HZAU.GP.DataMonitor.Entity;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using HZAU.GP.DataMonitor.Service.Biz;

namespace HZAU.GP.DataMonitor.Service
{
    public class DataMonitorService:BizServiceBase, IDataMonitorService
    {
        IDataMonitorBiz SchemeBiz;
        public DataMonitorService(IDataMonitorBiz SchemeBiz)
        {
            this.SchemeBiz = SchemeBiz;
        }
        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="SchemeEntity"></param>
        /// <returns></returns>
        public bool InsertScheme(SchemeEntity SchemeEntity)
        {
            return base.Do<IDataMonitorBiz, bool>(biz => biz.InsertScheme(SchemeEntity), this.SchemeBiz);
        }
        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public SchemeSearchResult SearchScheme(SchemeSearchCriteria criteria)
        {
            return base.Do<IDataMonitorBiz, SchemeSearchResult>(biz => biz.SearchScheme(criteria), this.SchemeBiz);
        }

        /// <summary>
        /// 导出方案的执行脚本数据到excel
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public string ExportSchemeToExcel(int pkId)
        {
            return base.Do<IDataMonitorBiz, string>(biz => biz.ExportSchemeToExcel(pkId), this.SchemeBiz);
        }

        /// <summary>
        /// 改变方案有效状态
        /// </summary>
        /// <param name="isEffective"></param>
        /// <returns></returns>
        public bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp)
        {
            return base.Do<IDataMonitorBiz, bool>(biz => biz.SetSchemeEffective(pkId, isEffective, stamp), this.SchemeBiz);
        }
    }
}
