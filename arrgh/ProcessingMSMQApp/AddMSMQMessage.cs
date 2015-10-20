using System.Messaging;
using System.Web.Configuration;
using Hitachi.MCRSWeb.Domain.Notifiers;
using System;
using System.Transactions;
using Newtonsoft.Json;
using System.Configuration;
using Hitachi.MCRS.Utility.Entities;
using Microsoft.SharePoint.Client.EventReceivers;

namespace ProcessingMSMQApp
{
    public class AddMSMQMessage
    {
        //Create queue and add MSMQ message
        static void Main(string[] args)
        {
            try
            {
                String queueName = ConfigurationManager.AppSettings["ProvisionQueueName"];
                var queue = new MessageQueue(queueName);
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                Console.WriteLine("Connecting to queue: " + queueName + "...Connection established.");
                while (true)
                {
                    SendMessage(queue);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Processing: " + e.Message);
                Console.ReadLine();
            }
        }

        private static void SendMessage(MessageQueue queue)
        {
            Console.WriteLine("Please type in your message: ");
            var msg = createMessage(Console.ReadLine());
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                queue.Send(msg, MessageQueueTransactionType.Automatic);
                ts.Complete();
            }
            Console.WriteLine("Message Sent");
        }

        private static Message createMessage(string v)
        {
            return new Message(
                JsonConvert.SerializeObject(new AppInstallEventMessage()
                {
                    properties = new SPRemoteEventProperties()
                    {
                        ContextToken = "NewContextToken",
                        ErrorCode = "ErrorCode",
                        ErrorMessage = "ErrorMessage",
                        UICultureLCID = 3
                    },
                    eventReceiverUrl = v
                })
             );

        }
    }
}
