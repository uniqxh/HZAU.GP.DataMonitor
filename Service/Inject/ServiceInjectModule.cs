using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using HZAU.GP.DataMonitor.Service.Contract;
using HZAU.GP.DataMonitor.Service;

namespace HZAU.GP.DataMonitor.Service.Inject
{
    public class ServiceInjectModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IDataMonitorService>().To<DataMonitorService>();
        }
        public override string Name
        {
            get
            {
                return "ServiceInjectModule";
            }
        }
    }
}
