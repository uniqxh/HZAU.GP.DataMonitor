using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

using HZAU.GP.DataMonitor.Web.Inject;

namespace HZAU.GP.DataMonitor.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(this.CreateKernel()));
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private IKernel CreateKernel()
        {
            INinjectModule[] modules = new INinjectModule[]
            {
                new ModelInjectModule()
            };
            return new StandardKernel(modules);
        }
    }
}