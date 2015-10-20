using System;
using System.Messaging;
using System.Configuration;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using AG;

namespace MSMQReceiverApp
{
    public class Program
    {
        private static int count = 0;
        static ManualResetEvent signal = new ManualResetEvent(false);
        //Get queue and receive MSMQ messages
        static void Main(string[] args)
        {
            try
            {
                string queueName = ConfigurationManager.AppSettings["ProvisionQueueName"];
                var queue = new MessageQueue(queueName);
               writeBoth("Queue Created at: in ReceiverApp: " + DateTime.Now);
                //Console.ReadLine();
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                //Below code reads from queue once

                Message[] messages = queue.GetAllMessages();
                writeBoth("Reading from queue in ReceiverApp: " + queueName);
                writeBoth("Number of messages in ReceiverApp: " + messages.Length);
                /**foreach (Message msg in messages)
                {
                   // var decoded = JsonConvert.DeserializeObject(msg.Body.ToString());
                    Console.WriteLine("message:" + msg.Body);
                    Console.ReadLine();
                }
                Console.WriteLine("End of messages");
                Console.ReadLine();
                **/
                //Below code keeps reading from queue
                queue.ReceiveCompleted += QueueMessageReceived;
                queue.BeginReceive();
                signal.WaitOne();
                //Console.ReadLine();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error in receiving in ReceiverApp: " + e.Message);
            }
        }

        private static void writeBoth(String what)
        {
            Trace.WriteLine(what);
            Console.WriteLine(what);
            Console.ReadLine();
        }

        private static void QueueMessageReceived(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue msQueue = (MessageQueue)sender;

                //once a message is received, stop receiving
                Message msMessage = msQueue.EndReceive(e.AsyncResult);

                //do something with the message
                writeBoth("Received message body in ReceiverApp: " + msMessage.Body + " at " + DateTime.Now);
                Functions.ProcessQueueMessage(msMessage.Body.ToString());
                //set the signal
                count += 1;
                if (count == 10)
                {
                    signal.Set();
                }
                //Restart the receive operation
                // Console.ReadLine();
                //begin receiving again
                msQueue.BeginReceive();
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.Message);
            }
        }
    }
}
