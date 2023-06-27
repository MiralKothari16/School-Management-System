using Student_Management_System.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.Interface
{
    public interface IMailsService
    {
        bool SendMail (MailDTO mail);
    }
}
