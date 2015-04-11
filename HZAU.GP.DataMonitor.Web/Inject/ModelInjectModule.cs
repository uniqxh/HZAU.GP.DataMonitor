using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using HZAU.GP.DataMonitor.Web.Models;

namespace HZAU.GP.DataMonitor.Web.Inject
{
    public class ModelInjectModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IDataMonitorModel>().To<DataMonitorModel>();
        }
        public override string Name
        {
            get
            {
                return "ModuleLoadingModule";
            }
        }
    }
}