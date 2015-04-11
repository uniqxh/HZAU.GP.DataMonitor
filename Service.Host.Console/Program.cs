using Ninject;
using Ninject.Modules;
using HZAU.GP.DataMonitor.Service.Inject;
using HZAU.GP.DataMonitor.Service.Host;

namespace Service.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = CreateKernel();
            var host = kernel.Get<HostManager>();
            host.run();
        }
        private static IKernel CreateKernel()
        {
            INinjectModule[] modules = new INinjectModule[]
            {
                new ServiceInjectModule(),
                new DataMonitorBizInjectModule(),
                new HostModule()
            };
            return new StandardKernel(modules);
        }
    }
}
