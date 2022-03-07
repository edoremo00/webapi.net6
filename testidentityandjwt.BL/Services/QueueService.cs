using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using testidentityandjwt.BL.Enums;
using testidentityandjwt.BL.IMapper;
using testidentityandjwt.DAL.Entities;

namespace testidentityandjwt.BL.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;
        private readonly IDatamapper _mapper;

        public QueueService(IConfiguration configuration, IDatamapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task SendMessagetoqueue<T>(T message, string queuename, Eventlabelsservicebus eventlabelname)//è generico va bene per varie classi
        {
            QueueClient queueClient = new QueueClient(_configuration.GetSection("Servicebus:Connectionstring").Value, _configuration.GetSection("Servicebus:queues:queuename").Value);
            Message m = new Message()
            {
                ContentType = "application/json",
                Body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)),
                Label = eventlabelname.ToString()

            };
            await queueClient.SendAsync(m);
            

        }

        /* static List<MyUser> messages=new List<MyUser>();

         public async void Sendmessagetoqueue()
         {
             IQueueClient client = new QueueClient(_configuration.GetSection("Servicebus:Connectionstring").Value, _configuration.GetSection("Servicebus:queues:queuename").Value);
             /*string message = "primo messaggio su coda";
             Message m = new Message(Encoding.UTF8.GetBytes(message));*/
        /*  MyUser user = new()
          {
              Email = "testcodeazure@ciao.com",
              UserName = "Edotestcode",
              Id = Guid.NewGuid().ToString()
          };
          Message m1 = new Message()
          {
              ContentType = "application/json",
              Body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user))
          };
          await client.SendAsync(m1);
      }

      public async Task<IEnumerable<object>> Getmessagesfromqueue()
      {
          //QueueClient client = new QueueClient(_configuration.GetSection("Servicebus:Connectionstring").Value, _configuration.GetSection("Servicebus:queues:queuename").Value);
          ServiceBusClient client = new ServiceBusClient(_configuration.GetSection("Servicebus:Connectionstring").Value);
          ServiceBusProcessor serviceBusProcessor;
          ServiceBusProcessorOptions serviceBusProcessorOptions=new ServiceBusProcessorOptions() { ReceiveMode=ServiceBusReceiveMode.PeekLock,AutoCompleteMessages=false};

          serviceBusProcessor = client.CreateProcessor(_configuration.GetSection("Servicebus:queues:queuename").Value,serviceBusProcessorOptions);

          serviceBusProcessor.ProcessMessageAsync += MessageHandler;
          serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

          await  serviceBusProcessor.StartProcessingAsync();

          Thread.Sleep(30000);//lo faccio processare per 30 secondi

          await serviceBusProcessor.StopProcessingAsync();

          return messages.Select(m=>_mapper.mapmyusertodto(m));
      }

      public async Task<object> MessageHandler(ProcessMessageEventArgs args)//viene chiamato quando arriva un messaggio nella coda
      {
          var body = args.Message.Body.ToObjectFromJson<MyUser>();
          //List<object> messages = new List<object>();
          messages.Add(body);


          // complete the message. messages is deleted from the queue. 
          //await args.CompleteMessageAsync(args.Message);
          //CHIAMANDO COMPLETEMESSAGEASYNC RIMUOVE I MESSAGGI DALLA CODA
          //messages.Select(u => _mapper.mapmyusertodto(u));
          return Task.CompletedTask;
      }

      // handle any errors when receiving messages
      public async Task<object> ErrorHandler(ProcessErrorEventArgs args)
      {
          /*QueueClient queueClient = new QueueClient(_configuration.GetSection("Servicebus:Connectionstring").Value, _configuration.GetSection("Servicebus:queues:queuename").Value);
          queueClient.AbandonAsync(args.)*/
        /*  Console.WriteLine(args.Exception.ToString());
          return Task.CompletedTask;
      }*/

    }
}
