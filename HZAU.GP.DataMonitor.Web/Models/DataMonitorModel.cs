using HZAU.GP.DataMonitor.Service.Contract;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;
using System;

namespace HZAU.GP.DataMonitor.Web.Models
{
    public class DataMonitorModel:WCFProxyModelsBase, IDataMonitorModel
    {
        public SchemeSearchResult SearchScheme(SchemeSearchCriteria SchemeSearchCriteria)
        {
            base.InitWCFClientProxy();
            return base.wcfClientProxy.Call<IDataMonitorService, SchemeSearchResult>(s => s.SearchScheme(SchemeSearchCriteria));
        }
        public bool InsertScheme(SchemeEntity entity)
        {
            base.InitWCFClientProxy();
            return base.wcfClientProxy.Call<IDataMonitorService, bool>(s => s.InsertScheme(entity));
        }
        public bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp)
        {
            base.InitWCFClientProxy();
            return base.wcfClientProxy.Call<IDataMonitorService, bool>(
                s => s.SetSchemeEffective(pkId, isEffective, stamp));
        }
        public string ExportSchemeToExcel(int pkId)
        {
            base.InitWCFClientProxy();
            return base.wcfClientProxy.Call<IDataMonitorService, string>(
                s => s.ExportSchemeToExcel(pkId));
        }
    }
}