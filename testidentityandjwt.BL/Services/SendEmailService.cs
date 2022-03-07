﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using testidentityandjwt.BL.Enums;
using testidentityandjwt.BL.Utils;

namespace testidentityandjwt.BL.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<object> SendEmail(string email, Emailsubjects emailsubjects)
        {
            if (String.IsNullOrWhiteSpace(email))
                return new Error() { Message="email is required",StatusCode=StatusCodes.Status400BadRequest};
            else if(!Validateemailstructure(email))
                return new Error() { Message="invalid email format must be written like aaaa@bbb.com",StatusCode =StatusCodes.Status400BadRequest};
            string APIKEY = _configuration.GetSection("SendGrid:APIKEY").Value;
            SendGridClient clientemail = new SendGridClient(APIKEY);

            SendGridMessage emailmessage = new SendGridMessage()
            {
                From = new EmailAddress(_configuration.GetSection("SendGrid:Email").Value),
                Subject = emailsubjects.ToString(),
                PlainTextContent = $"hi {email.Remove(email.IndexOf('@'),email.Length-email.IndexOf('@'))},{bodycontent(emailsubjects)}" 

            };
            emailmessage.AddTo(email);
            var response = await clientemail.SendEmailAsync(emailmessage);
            return response.IsSuccessStatusCode ? JsonSerializer.Serialize<bool>(true) :new Error(){Message="error in sending email",StatusCode=500 };
        }

        //overload metodo precedente per avere anche parametro body
        public async Task<bool> SendEmail(string email, Emailsubjects emailsubjects,string bodycontent)
        {
            if (String.IsNullOrWhiteSpace(email))
                return false;
            else if (String.IsNullOrWhiteSpace(bodycontent))
                return false;
            string APIKEY = _configuration.GetSection("SendGrid:APIKEY").Value;
            SendGridClient clientemail = new SendGridClient(APIKEY);
            SendGridMessage emailmessage = new SendGridMessage()
            {
                From = new EmailAddress(_configuration.GetSection("SendGrid:Email").Value),
                Subject = emailsubjects.ToString(),
                PlainTextContent = bodycontent

            };
            emailmessage.AddTo(email);
            var response = await clientemail.SendEmailAsync(emailmessage);
            return response.IsSuccessStatusCode ? true : false;
        }

        private bool  Validateemailstructure(string emailtovalidate)
        {
            try
            {
                MailAddress email=new MailAddress(emailtovalidate);
                return true;
            }catch (Exception ex)
            {

                return false;
            }
        }

        private string bodycontent(Emailsubjects subject)
        {
            switch (subject)
            {
                case Emailsubjects.Resetpassword:
                    return "Your password has been resetted";
                case Emailsubjects.Welcomeemail:
                    return "welcome to todo crud API";
                case Emailsubjects.Deletedaccount:
                    return "your account has been deleted";
                case Emailsubjects.password_has_been_changed:
                    return "your password has been changed";
                    break;
                default:
                    return String.Empty;
                    
            }
        }

       
    }
}
