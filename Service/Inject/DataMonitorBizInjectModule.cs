using Ninject.Modules;
using HZAU.GP.DataMonitor.Service.Biz;

namespace HZAU.GP.DataMonitor.Service.Inject
{
    public class DataMonitorBizInjectModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IDataMonitorBiz>().To<DefaultDataMonitorBiz>();
            Bind<ICreateDataMonitorBiz>().To<CreateDataMonitorBiz>();
            Bind<IGetDataMonitorBiz>().To<GetDataMonitorBiz>();
            Bind<IUpdateSchemeBiz>().To<UpdateSchemeBiz>();
            Bind<IExecuteSchemeProvider>().To<DefaultExecuteSchemeProvider>();
            Bind<IConnection>().To<Connection>();
            Bind<IExcelServer>().To<ExcelServer>();
        }

        public override string Name
        {
            get
            {
                return "DataMonitorBizInjectModule";
            }
        }
    }
}
