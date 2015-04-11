using HZAU.GP.DataMonitor.Entity.BizEntity;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IExecuteScheme
    {
        string ExportExecuteResultToExcel(SchemeEntity entity);
    }
}
