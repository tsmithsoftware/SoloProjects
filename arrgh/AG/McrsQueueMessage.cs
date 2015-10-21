using System.Runtime.Serialization;

namespace AG
{
    [DataContract(Name = "McrsQueueMessage", Namespace = "http://schemas.class-a.nl/msmq/example01/2008/02/")]
    public class McrsQueueMessage : IQueueMessage
    {
        public McrsQueueMessage()
        {

        }

        public McrsQueueMessage(string message)
        {
            AsString = message;
        }

        public string AsString { get; set; }
    }
}