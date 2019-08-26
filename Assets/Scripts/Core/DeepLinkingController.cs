using System;
using Firebase.Extensions;
using UnityEngine;

namespace Firebase.Sample.DynamicLinksFolder
{
    public class DeepLinkingController : IDeepLinkingController
    {
        public DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;

        public void Init(IDeepLinkLReceiver deepLinkReceiver, string domain)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                _dependencyStatus = task.Result;
                if (_dependencyStatus == DependencyStatus.Available)
                {
                    DynamicLinks.DynamicLinks.DynamicLinkReceived += deepLinkReceiver.OnDynamicLink;
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + _dependencyStatus);
                }
            });
        }
    }
}