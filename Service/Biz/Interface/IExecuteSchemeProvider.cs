using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IExecuteSchemeProvider
    {
        IExecuteScheme GetSchemeExecuteType(string executetype, IConnection con);
    }
}
