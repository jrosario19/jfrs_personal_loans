using jfrs_personal_loans.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services
{
    public class EmailService: IEmailService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";
        private readonly SMTPConfig _smtpConfig;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("{{UserName}}, por favor verifique su Email", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ConfirmEmail"), userEmailOptions.Placeholders);
            //userEmailOptions.Body = "Juan Rosario";
             await SendEmailAsync(userEmailOptions);
        }

        public async Task SendEmailForResetPassword(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("{{UserName}}, Reestablecimiento de Clave Token", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ResetPasswordEmail"), userEmailOptions.Placeholders);

            await SendEmailAsync(userEmailOptions);
        }

        public async Task SendEmailForEmailConfirmationSuccess(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("{{UserName}}, Verificación de Email Exitosa", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("SuccessfullEmailConfirmation"), userEmailOptions.Placeholders);

            await SendEmailAsync(userEmailOptions);
        }

        public async Task SendEmailForEmailConfirmationFail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("{{UserName}}, Falla en la vefición de Email", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("FailureEmailConfirmation"), userEmailOptions.Placeholders);

            await SendEmailAsync(userEmailOptions);
        }

        public EmailService(IOptions<SMTPConfig> smtpConfig, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this._smtpConfig = smtpConfig.Value;
            this._configuration = configuration;
            this._hostingEnvironment = hostingEnvironment;
        }

        private async Task SendEmailAsync(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML,
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_configuration["Email_user_name"], _configuration["Email_password"]);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(Path.Combine(_hostingEnvironment.WebRootPath,string.Format(templatePath, templateName)));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs !=null)
            {
                foreach (var placeHolder in keyValuePairs)
                {
                    if (text.Contains(placeHolder.Key))
                    {
                        text = text.Replace(placeHolder.Key, placeHolder.Value);
                    }
                }
            }

            return text;
        }

        
    }
}
