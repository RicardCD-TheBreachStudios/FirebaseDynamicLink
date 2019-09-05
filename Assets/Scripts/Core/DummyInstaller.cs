using Firebase.Sample.DynamicLinksFolder;
using UnityEngine;

namespace Core
{
    public class DummyInstaller : MonoBehaviour
    {
        public ShowLink _showLink;
        public string Domain;
        
        void Start()
        {
            var listener = new DeepLinkReceiver(_showLink.Handle);
            var controller = new DeepLinkingController();
            controller.Init(listener, Domain);
        }
    }
}