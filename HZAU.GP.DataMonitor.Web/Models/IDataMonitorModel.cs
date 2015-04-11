using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using System;

namespace HZAU.GP.DataMonitor.Web.Models
{
    public interface IDataMonitorModel
    {
        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="SchemeSearchCriteria"></param>
        /// <returns></returns>
        SchemeSearchResult SearchScheme(SchemeSearchCriteria SchemeSearchCriteria);
        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="entity"></param>
        bool InsertScheme(SchemeEntity entity);
        /// <summary>
        /// 设置effective
        /// </summary>
        /// <param name="schemeId"></param>
        /// <param name="isEffective"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp);

        string ExportSchemeToExcel(int pkId);
    }
}
