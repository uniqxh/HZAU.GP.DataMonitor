using System;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IUpdateSchemeBiz
    {
        bool SetSchemeEffective(int pkId, int isEffective, DateTime stamp);
    }
}
