using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HZAU.GP.DataMonitor.Web.Models.WCF;

namespace HZAU.GP.DataMonitor.Web.Models
{
    public class WCFProxyModelsBase
    {
        protected WCFClientProxy wcfClientProxy;
        protected void InitWCFClientProxy()
        {
            this.wcfClientProxy = new WCFClientProxy();
        }
    }
}