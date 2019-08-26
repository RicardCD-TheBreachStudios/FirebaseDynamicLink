using System;
using Firebase.DynamicLinks;

namespace Firebase.Sample.DynamicLinksFolder
{
    public interface IDeepLinkReceiver
    {
        event Action<ReceivedDynamicLinkEventArgs> OnDeepLinkReceived;
    }
    
}