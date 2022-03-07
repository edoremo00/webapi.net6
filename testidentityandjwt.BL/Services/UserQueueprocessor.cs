using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.Services
{
    public class UserQueueprocessor
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserQueueprocessor> _logger;

        public UserQueueprocessor(IConfiguration configuration,ILogger<UserQueueprocessor> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public void Processuserregistrationqueue()
        {
            QueueClient queueClient = new QueueClient(_configuration.GetSection("Servicebus:Connectionstring").Value, _configuration.GetSection("Servicebus:queues:queuename").Value);
            var messagehandleroptions = new MessageHandlerOptions(Exceptionreceivedhandler)
            {
                AutoComplete = true,
                MaxConcurrentCalls = 1
            };
            
            queueClient.RegisterMessageHandler(Sendemailtoregistereduser, messagehandleroptions);//metodo che viene chiamato quando arrivano messaggi in queue
        }

        private async Task Sendemailtoregistereduser(Message messagereceived, CancellationToken arg2)
        {
            string jsonbodymessage=Encoding.UTF8.GetString(messagereceived.Body);
           
           
            //await SendEmailtosubscribeduser(usertosendemailto!.Email);

           
            
        }

        private Task Exceptionreceivedhandler(ExceptionReceivedEventArgs arg)
        {
            _logger.LogError(arg.Exception.Message);
            return Task.CompletedTask;
        }

      /* private async Task<bool> SendEmailtosubscribeduser(string useremail)
        {
            string otp = Generateotp();
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential()
                {
                    //UserName = _configuration["Emailconfig:username"],
                    //Password = _configuration["Emailconfig:password"]
                    //UserName=config["Username"],
                    UserName = "libreriabeemail@gmail.com",
                    Password = "Remoedo2000",

                };
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                MailAddress from = new MailAddress("libreriabeemail@gmail.com");
                MailAddress to = new MailAddress(useremail);
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Welcome to todocrud";
                message.Body = $"this is the otp code {otp}";
                message.Priority = MailPriority.High;

                try
                {
                    await smtp.SendMailAsync(from.Address, to.Address, message.Subject, message.Body);
                    
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }



            }
        }

        private string Generateotp()
        {
            Random r = new Random();
            return r.Next(100000, 999999).ToString();
            
        }*/
        
    }
}
