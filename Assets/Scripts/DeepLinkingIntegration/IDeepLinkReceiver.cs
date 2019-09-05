using System;
using Firebase.DynamicLinks;

namespace Firebase.Sample.DynamicLinksFolder
{
    public interface IDeepLinkLReceiver   
    {
        void OnDynamicLink(object sender, EventArgs args);
    }
    
}