using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.ServiceBus.Messaging;

namespace EventSender
{
    class Program
    {
        //this is name of your eventhub found under Entities -> Eventhubs
        static string eventHubName = "";
        // go to Shared access policies under eventhub namespaces, click on RootManageSharedAccessKey and
        //copy Connection string–primary key. Note you need to copy Endpoint=sb:// too
        static string connectionString = "";
        
        static void SendingRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            Int64 ctr = 0;
            while (true)
            {
        
                try
                {
                    var message = Guid.NewGuid().ToString();
                    Console.WriteLine("{0} - {1} > Sending message: {2}",ctr++, DateTime.Now, message);
                    eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }
                //Thread.Sleep(2000);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl-C to stop the sender process");
            Console.WriteLine("Press Enter to start now");
            Console.ReadLine();
            SendingRandomMessages();
        }
    }
}
