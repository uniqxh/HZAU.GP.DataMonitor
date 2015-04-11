using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using HZAU.GP.DataMonitor.Service.Host;

namespace HZAU.GP.DataMonitor.Service.Host
{
    public class HostModule: NinjectModule
    {
        public override void Load()
        {
            Bind<HostManager>().ToSelf();
        }
        public override string Name
        {
            get
            {
                return "HostModule";
            }
        }
    }
}
