using System;
using System.Diagnostics;
using System.ServiceModel.MsmqIntegration;

namespace AG
{
    public class SendMailService : ISendMail
    {
        public void SubmitMessage(MsmqMessage<McrsQueueMessage> message)
        {
            Trace.WriteLine("In SubmitMessage");
            Trace.WriteLine(message.Body.AsString);
        }

        public void SubmitMessage(McrsQueueMessage message)
        {
            Trace.WriteLine("In SubmitMessage");
            Trace.WriteLine(message.AsString);
        }
    }
}
