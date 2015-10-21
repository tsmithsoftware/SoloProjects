using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Configuration;
using AG;
using System.ServiceModel;
using System.Diagnostics;
using MSMQReceiverService;

namespace HostApplication
{
    //This is going to act as a host for both the Service in AG and MSMQ ReceiverService. MSMSQReceiverService is, I think, what is going into MCRS.
    public class Program
    {
        //Hosting AG Service or MSMQReceiver, depending on values in App.Conf/MSMQService
        static void Main(string[] args)
        {
            try
            {
                string queueName = ConfigurationManager.AppSettings["QueueName"];
                bool msmq = Boolean.Parse(ConfigurationManager.AppSettings["MSMQService"]);
                Trace.WriteLine(queueName + " opened in aarghHostApp... at " + DateTime.Now);
                if (!MessageQueue.Exists(queueName))
                {
                    MessageQueue.Create(queueName, true);
                }

                Type serviceType = null;
                if (!msmq)
                {
                    serviceType = typeof(SendMailService);
                }
                else
                {
                    serviceType = typeof(MSMQService);
                }
                using (ServiceHost host = new ServiceHost(serviceType))
                {
                    Trace.WriteLine("opening host in aarghHostApp..."+DateTime.Now);
                    host.Open();
                }
            }
            catch(Exception e)
            {
                Trace.WriteLine("Error: " + e.Message);
            }
        }
    }
}
