using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System;

namespace Globomantics.Areas.Identity;

public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //SmtpClient
            //SendGrid
            return Task.CompletedTask;
        }
    }
