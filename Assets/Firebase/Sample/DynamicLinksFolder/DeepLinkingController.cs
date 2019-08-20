using System;

namespace Firebase.Sample.DynamicLinksFolder
{
    public class DeepLinkingController : IDeepLinkingController
    {
        public void Init(IDeepLinkListener listener, string domain)
        {
            //TODO: cant use assemblies
            //DynamicLinks += listener;
        }
    }
}