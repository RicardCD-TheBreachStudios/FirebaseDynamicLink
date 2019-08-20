using System;
using Firebase.DynamicLinks;

namespace Firebase.Sample.DynamicLinksFolder
{
    public interface IDeepLinkListener
    {
        event Action<ReceivedDynamicLinkEventArgs> OnDeepLinkReceived;
    }
    
}