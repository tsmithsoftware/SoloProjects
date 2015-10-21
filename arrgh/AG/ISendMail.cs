using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace AG
{
    [ServiceContract(Namespace ="http://schemas.class-a.nl/msmq/example01/2008/02/", SessionMode=SessionMode.NotAllowed)]
        public interface ISendMail
    {
        [OperationContract(Name = "SubmitMessage", IsOneWay = true)]
        void SubmitMessage(MsmqMessage<McrsQueueMessage> message);
    }
}