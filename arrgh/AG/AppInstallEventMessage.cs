using Microsoft.SharePoint.Client.EventReceivers;

namespace AG
{
    public class AppInstallEventMessage
    {
        public SPRemoteEventProperties properties { get; set; }
        public string eventReceiverUrl { get; set; }
    }
}