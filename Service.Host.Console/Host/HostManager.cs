using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using HZAU.GP.DataMonitor.Service;
using HZAU.GP.DataMonitor.Service.Contract;
using Ninject.Extensions.Wcf;

namespace HZAU.GP.DataMonitor.Service.Host
{
    public class HostManager
    {
        protected ServiceHost host = null;
        public HostManager(NinjectServiceHost<DataMonitorService> _host){
            this.host = _host;
            this.host.AddServiceEndpoint(typeof(IDataMonitorService), new NetTcpBinding(), new Uri("net.tcp://localhost:10000"));
            this.host.OpenTimeout = new TimeSpan(1, 0, 0);
        }
        public void run()
        {
            host.Open();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Start Service On: net.tcp://localhost:10000");
            Console.ReadLine();
        }
    }
}
