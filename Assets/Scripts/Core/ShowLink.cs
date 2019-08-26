using Firebase.DynamicLinks;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShowLink : MonoBehaviour
    {
        public Text _linkUrl;
        public void Handle(ReceivedDynamicLinkEventArgs args)
        {
            _linkUrl.text = args.ReceivedDynamicLink.Url.OriginalString;
        }
    }
}