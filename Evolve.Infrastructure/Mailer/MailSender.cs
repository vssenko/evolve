using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.Mailer
{
    public class MailSender
    {
        readonly string _email;
        readonly string _password;

        public MailSender(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public async void  SendEmail(string title,string body, string address)
        {
            using(SmtpClient client = new SmtpClient())
            {
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_email, _password);

                using(MailMessage mm = new MailMessage(_email, address, title, body))
                {
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    await client.SendMailAsync(mm);
                }
            }
        }


    }
}
