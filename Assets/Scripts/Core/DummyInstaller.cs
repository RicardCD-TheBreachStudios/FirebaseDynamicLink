using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.DynamicLinks;
using Firebase.Sample.DynamicLinksFolder;
using UnityEngine;

namespace Core
{
    public class DummyInstaller : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var listener = new DeepLinkingListener();
            var controller = new DeepLinkingController();
            controller.Init(listener, "deeplinktest1234.page.link");
        }
    }

   
    
}