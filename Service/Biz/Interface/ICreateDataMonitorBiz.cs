using System;
using System.Collections.Generic;
using HZAU.GP.DataMonitor.Entity.BizEntity;
using HZAU.GP.DataMonitor.Entity.SearchResult;
using HZAU.GP.DataMonitor.Entity.SearchCriteria;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface ICreateDataMonitorBiz
    {
        bool InsertScheme(SchemeEntity SchemeEntity);
    }
}
