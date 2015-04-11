using System;
using System.Collections.Generic;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IDataMonitorBiz
    {
        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="SchemeEntity"></param>
        /// <returns></returns>
        bool InsertScheme(SchemeEntity SchemeEntity);

        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        SchemeSearchResult SearchScheme(SchemeSearchCriteria criteria);

        string ExportSchemeToExcel(int pkId);
        bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp);
    }
}
