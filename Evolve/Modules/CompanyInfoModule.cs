using Evolve.Infrastructure.Mailer;
using Nancy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Modules
{
    public class CompanyInfoModule : NancyModule
    {
        public CompanyInfoModule()
        {
            Get["/contact"] = _ => View["Contact/contact.sshtml"];

            Get["/about"] = _ => View["About.sshtml"];

            Post["/contact", true] = async ( _ , ct) =>
                {
                    MailSender ms = new MailSender(ConfigurationManager.AppSettings["emailClientName"], ConfigurationManager.AppSettings["emailClientPassword"]);
                    ms.SendEmail(String.Format("Email from {0} named {1}.", Request.Form.email, Request.Form.name), Request.Form.message, ConfigurationManager.AppSettings["recipientEmail"]);
                    return HttpStatusCode.OK;
                };
        }
    }
}
