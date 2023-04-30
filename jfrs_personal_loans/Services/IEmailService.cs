using jfrs_personal_loans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Services
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmationSuccess(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmationFail(UserEmailOptions userEmailOptions);
    }
}
