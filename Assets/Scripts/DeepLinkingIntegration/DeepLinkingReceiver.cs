using System;
using Firebase.DynamicLinks;

namespace Firebase.Sample.DynamicLinksFolder
{
    public class DeepLinkReceiver : IDeepLinkLReceiver
    {
        private readonly Action<ReceivedDynamicLinkEventArgs> _objectReceiver;
        public DeepLinkReceiver(Action<ReceivedDynamicLinkEventArgs> objectReceiver)
        {
            _objectReceiver = objectReceiver;
        }

        public void OnDynamicLink(object sender, EventArgs args)
        {
            var dynamicLinkEventArgs = args as ReceivedDynamicLinkEventArgs;
            _objectReceiver.Invoke(dynamicLinkEventArgs);
        }

    }
}