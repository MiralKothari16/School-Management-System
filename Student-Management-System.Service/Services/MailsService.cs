using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Student_Management_System.Service.Configuration;
using Student_Management_System.Service.DTO;
using Student_Management_System.Service.Interface;

namespace Student_Management_System.Service.Services
{
    public class MailsService : IMailsService
    {

        #region Fields
        private readonly EmailConfiguration _emailConfiguration;
        #endregion
        #region COnstr
        public MailsService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        #endregion
        #region Method

        public bool SendMail(MailDTO email)
        {
            //createMail
            var emailmsg = new MimeMessage();
            emailmsg.From.Add(new MailboxAddress(_emailConfiguration.DisplayName, _emailConfiguration.From));
            emailmsg.To.Add(MailboxAddress.Parse(email.To));
            email.Subject = email.Subject;
            var bodybuilder = new BodyBuilder();
            bodybuilder.HtmlBody = email.Body;
            emailmsg.Body = bodybuilder.ToMessageBody();

            //SendMail
            bool Flag;
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect(_emailConfiguration.Host, _emailConfiguration.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_emailConfiguration.From, _emailConfiguration.Password);
                    smtp.Send(emailmsg);
                    Flag = true;
                }
                catch (Exception ex)
                {
                    Flag = false;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
            return Flag;
        }
        #endregion
    }
}
