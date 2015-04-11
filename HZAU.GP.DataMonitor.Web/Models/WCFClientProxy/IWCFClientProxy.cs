using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZAU.GP.DataMonitor.Web.Models.WCF
{
    public interface IWCFClientProxy
    {
        void Call<TChannel>(Action<TChannel> action);
        TReturn Call<TChannel, TReturn>(Func<TChannel, TReturn> fun);
    }
}
