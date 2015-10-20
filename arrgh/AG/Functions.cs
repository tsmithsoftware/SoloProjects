using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace AG
{
    public class Functions
    {
        public static void ProcessQueueMessage(string stringProperties)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<AppInstallEventMessage>(stringProperties);
                var properties = message.properties;
                Trace.Write("Starting to process message for site {0}", "mysite");

                var clientId = 1;
                var clientSecret = 2;
                Execute(clientId,clientSecret,"param3");
            }
            catch (Exception exception)
            {
                Trace.Write(exception.Message);
            }
        }

        private static void Execute(object appEventProperties, object eventReceiverUrl, String otherClass)
        {
            Trace.WriteLine("Execute function called.");
        }
    }
}
