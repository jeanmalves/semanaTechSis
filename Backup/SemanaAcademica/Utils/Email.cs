using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace SemanaAcademica.Utils
{
    public class Email
    {
        public bool Send(string address, string subject, string message, bool html = false)
        {

            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage m = new MailMessage(
                    "seminfo@dainf.ct.utfpr.edu.br", // From
                    address, // To
                    subject, // Subject
                    message); // Body
                m.IsBodyHtml = html;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtpClient.Send(m);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}