using System.Net.Mail;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface ISmtpMailer
    {
        void SendEmail(string subject, string body, string from, params string[] to);
    }

    public class DefaultMailer : ISmtpMailer
    {
        public void SendEmail(string subject, string body, string from, params string[] to)
        {
            var mailMessage = new MailMessage
            {
                IsBodyHtml = true,
                Body = body,
                Subject = subject
            };
            mailMessage.To.Add(string.Join(",", to));
            mailMessage.From = new MailAddress(from);

            var client = new SmtpClient();
            client.Send(mailMessage);
        }
    }
}