using Microsoft.AspNetCore.Http;
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
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.Services
{
 

    //SUBSCRIBER CLASS. IT SUBSCRIBES TO THE EVENT EMITTED FROM USERAUTHSERVICE CLASS
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

       /* public async Task<object> SendEmailtoRegisteredUser(string email)
        {
            return SendEmail(email,Emailsubjects.Welcomeemail).GetAwaiter().GetResult();
        }*/

        public object OnRegisteredUser(object source,UserArgs userargs)//DECLARE A METHOD WITH THE SAME SIGNATURE AS THE PUBLISHER DELEGATE
        {
            //return SendEmailtoRegisteredUser(userargs.Email).GetAwaiter().GetResult();
            return SendEmail(userargs.Email, Emailsubjects.Welcomeemail).GetAwaiter().GetResult();
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

       /* public Task<object> OnRegisteredUser(object source, EventArgs args)
        {
            // Console.WriteLine("sending email to registered user");

            //not sure what you want to return here...
            // return Task.FromResult(new object());

            return SendEmail("gamer200058@gmail.com", Emailsubjects.Welcomeemail);
        }

        //THIS IS THE METHOD WHICH IS NOT BEING CALLED BY THE EVENTHANDLER
        public async Task<object> OnUserregistered(object source,EventArgs args)
        {
           return  await SendEmail("gamer200058@gmail.com", Emailsubjects.Welcomeemail);
        }*/

       

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
