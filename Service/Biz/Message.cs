using System.Net.Mail;
using System.Text;

namespace HZAU.GP.DataMonitor.Service.Biz
{
    public class Message : IMessage
    {
        #region
        const string user = "emailforemail@163.com";
        const string pwd = "525799145";
        #endregion
        public void sendMail(string toMail, string fromMail, string subject, string emailBody, string attachment)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toMail);
                mail.From = new MailAddress(fromMail, "数据监控系统", Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = emailBody;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = false;
                mail.Priority = MailPriority.High;
                Attachment att = new Attachment(attachment);
                mail.Attachments.Add(att);
                SmtpClient client = new SmtpClient("smtp.163.com", 25);
                client.Credentials = new System.Net.NetworkCredential(user, pwd);
                client.EnableSsl = true;
                object userState = mail;
                client.SendAsync(mail, userState);
            }
            catch (SmtpException e)
            {
                throw e;
            }
        }
    }
}
