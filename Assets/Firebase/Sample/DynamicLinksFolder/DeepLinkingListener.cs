using System;
using Firebase.DynamicLinks;

namespace Firebase.Sample.DynamicLinksFolder
{
    public class DeepLinkingListener : IDeepLinkListener
    {
        public event Action<ReceivedDynamicLinkEventArgs> OnDeepLinkReceived;
    }
}