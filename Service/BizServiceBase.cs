using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZAU.GP.DataMonitor.Service.Biz;

namespace HZAU.GP.DataMonitor.Service
{
    public class BizServiceBase
    {
        public BizServiceBase() { }
        protected TReturn Do<TBiz, TReturn>(Func<TBiz, TReturn> func, TBiz biz)
        {
            try
            {
                return func.Invoke(biz);
            }
            catch (Exception e)
            {
                return default(TReturn);
            }
        }
    }
}
