using Firebase.Sample.DynamicLinksFolder;
using UnityEngine;

namespace Core
{
    public class DummyInstaller : MonoBehaviour
    {
        public ShowLink _showLink;
        // Start is called before the first frame update
        void Start()
        {
            var listener = new DeepLinkReceiver(_showLink.Handle);
            var controller = new DeepLinkingController();
            controller.Init(listener, "deeplinktest1234.page.link");

        }
    }
}