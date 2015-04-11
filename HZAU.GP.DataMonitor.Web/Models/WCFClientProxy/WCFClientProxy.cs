using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace HZAU.GP.DataMonitor.Web.Models.WCF
{
    public class WCFClientProxy: IWCFClientProxy
    {
        private static WCFClientProxy current = new WCFClientProxy();
        public static WCFClientProxy Current
        {
            get { return current; }
        }
        public WCFClientProxy() { }

        public void Call<TChannel>(Action<TChannel> action)
        {
            var chanFactory = new ChannelFactory<TChannel>(new NetTcpBinding(), "net.tcp://localhost:10000");
            chanFactory.Endpoint.Binding.SendTimeout = new TimeSpan(1, 0, 0);
            chanFactory.Endpoint.Binding.OpenTimeout = new TimeSpan(1, 0, 0);
            chanFactory.Endpoint.Binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
            TChannel channel = chanFactory.CreateChannel();
            try
            {
                ((IClientChannel)channel).Open();
                action(channel);
                ((IClientChannel)channel).Close();
            }
            catch(Exception e)
            {
                ((IClientChannel)channel).Abort();
                throw e;
            }
        }
        public TReturn Call<TChannel, TReturn>(Func<TChannel, TReturn> func)
        {
            var chanFactory = new ChannelFactory<TChannel>(new NetTcpBinding(), "net.tcp://localhost:10000");
            chanFactory.Endpoint.Binding.SendTimeout = new TimeSpan(1, 0, 0);
            chanFactory.Endpoint.Binding.OpenTimeout = new TimeSpan(1, 0, 0);
            chanFactory.Endpoint.Binding.ReceiveTimeout = new TimeSpan(1, 0, 0);
            TChannel channel = chanFactory.CreateChannel();
            try
            {
                ((IClientChannel)channel).Open();
                TReturn result = func(channel);
                ((IClientChannel)channel).Close();
                return result;
            }
            catch(Exception e)
            {
                ((IClientChannel)channel).Abort();
                throw e;
            }
        }
    }
}