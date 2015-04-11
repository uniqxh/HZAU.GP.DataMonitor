using System;
using System.Collections.Generic;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IGetDataMonitorBiz
    {
        SchemeEntity GetSchemeEntityById(int pkId);

        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        SchemeSearchResult SearchScheme(SchemeSearchCriteria criteria);

        string ExportSchemeToExcel(int pkId);
    }
}
