using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Core.Interfaces.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(string MailTo, string subject, string body, IList<IFormFile> attachments = null);

    }
}
