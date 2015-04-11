using System.Web;
using System.Web.Mvc;

namespace HZAU.GP.DataMonitor.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}