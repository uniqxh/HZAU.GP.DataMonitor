using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public interface IMessage
    {
        void sendMail(string toMail, string fromMail, string subject, string emailBody, string attachment);
    }
}
