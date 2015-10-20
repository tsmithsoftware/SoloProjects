using System;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using System.Messaging;
using System.Diagnostics;
using AG;

namespace MSMQReceiverService
{
    /**
    **Install as a service
    **/
    public class MSMQService:ServiceBase
    {
        private static int count = 0;
        static ManualResetEvent signal = new ManualResetEvent(false);

        public MSMQService()
        {
            this.ServiceName = "MCRS MSMQ Receiver Service";
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
        }

        public static void Main(string[] args)
        {
            Run(new MSMQService());
        }

        private void example()
        {
            /**private void DoWork()
            var worker = new Thread(DoWork);
            worker.Name = "MyWorker";
            worker.IsBackground = false;
            worker.Start();**/
            
        }

    protected override void OnStart(string[] args)
    {
            try
            {
                string queueName = ConfigurationManager.AppSettings["ProvisionQueueName"];
                //if (!MessageQueue.Exists(queueName))MessageQueue.Create(queueName, true);
                var queue = new MessageQueue(queueName);
                Trace.WriteLine("Queue Created in MSMQService at:" + DateTime.Now);
                //Console.ReadLine();
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                //Below code reads from queue once

                Message[] messages = queue.GetAllMessages();
                Trace.WriteLine("Reading from queue in MSMQService : " + queueName);
                Trace.WriteLine("Number of messages in MSMQService: " + messages.Length);
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
                Trace.WriteLine("Error in receiving in MSMQService: " + e.Message);
            }
        }

        private static void QueueMessageReceived(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue msQueue = (MessageQueue)sender;

                //once a message is received, stop receiving
                Message msMessage = msQueue.EndReceive(e.AsyncResult);

                //do something with the message
                Trace.WriteLine("Received message body in MSMQService: " + msMessage.Body + " at " + DateTime.Now);
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

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
