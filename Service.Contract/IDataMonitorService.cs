using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using System.ServiceModel;

namespace HZAU.GP.DataMonitor.Service.Contract
{
    /// <summary>
    /// 数据监控
    /// </summary>
    [ServiceContract(Namespace = "HZAU.GP.DataMonitor")]
    //[DefaultCustomerContractBehavior()]
    public interface IDataMonitorService
    {
        /// <summary>
        /// 创建方案
        /// </summary>
        /// <param name="SchemeEntity"></param>
        /// <returns></returns>
        [OperationContract]
        bool InsertScheme(SchemeEntity SchemeEntity);

        /// <summary>
        /// 查询方案
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [OperationContract]
        SchemeSearchResult SearchScheme(SchemeSearchCriteria criteria);
        [OperationContract]
        string ExportSchemeToExcel(int pkId);

        /// <summary>
        /// 改变方案有效状态
        /// </summary>
        /// <param name="isEffective"></param>
        /// <returns></returns>
        [OperationContract]
        bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp);
    }
}
