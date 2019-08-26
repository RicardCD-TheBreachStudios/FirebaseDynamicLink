using System;
using Firebase.DynamicLinks;

namespace Firebase.Sample.DynamicLinksFolder
{
    public class DeepLinkingReceiver : IDeepLinkReceiver
    {
        public event Action<ReceivedDynamicLinkEventArgs> OnDeepLinkReceived;
    }
}