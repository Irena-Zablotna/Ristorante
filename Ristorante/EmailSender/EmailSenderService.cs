using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ristorante.EmailSender
{
    public class EmailSenderService : IEmailSenderService
    {

        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IFluentEmail fluentEmail, ILogger<EmailSenderService> logger)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
        }


        public async Task<bool> Send(string to, string subject, string name)
        {
            var result = await _fluentEmail.To(to)
                 .Subject(subject)
                 .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/EmailSender/Templates/WelcomeMessage.cshtml", new { UserName = name })
                 .SendAsync();

            if(!result.Successful)
            {
                _logger.LogError("Failed to send email\n {Errors}", string.Join(',', result.ErrorMessages));
               
            }
            return result.Successful;
        }
    }
}
