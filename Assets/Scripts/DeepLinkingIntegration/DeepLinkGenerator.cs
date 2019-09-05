using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.DynamicLinks;
using Firebase.Extensions;
using UnityEngine;

namespace DeepLinkingIntegration
{
    public class DeepLinkGenerator : IDeepLinkGenerator
    {
        public Uri GenerateLongLink(Uri uriLink, string domain,
            DynamicLinkParametersConfiguration dynamicLinkParametersConfiguration)
        {
            var components = CreateDynamicLinkComponents(uriLink, domain, dynamicLinkParametersConfiguration);

            return components.LongDynamicLink;
        }

        public async Task<ShortDynamicLink> GenerateShortLink(Uri uri, string domain,
            DynamicLinkParametersConfiguration dynamicLinkParametersConfiguration, DynamicLinkOptions options)
        {
            return await CreateShortLinkAsync(uri, domain, dynamicLinkParametersConfiguration, options);
        }

        private Task<ShortDynamicLink> CreateShortLinkAsync(Uri uri, string domain,
            DynamicLinkParametersConfiguration dynamicLinkParametersConfiguration, DynamicLinkOptions options)
        {
            var components = CreateDynamicLinkComponents(uri, domain, dynamicLinkParametersConfiguration);
            return Firebase.DynamicLinks.DynamicLinks.GetShortLinkAsync(components, options)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.Log("Short link creation canceled");
                    }
                    else if (task.IsFaulted)
                    {
                        if (task.Exception != null)
                            Debug.Log($"Short link creation failed {task.Exception.ToString()}");
                    }
                    else
                    {
                        var link = task.Result;
                        Debug.Log($"Generated short link {link.Url}");
                        var warnings = new List<string>(link.Warnings);
                        if (warnings.Count > 0)
                        {
                            Debug.Log("Warnings:");
                            foreach (var warning in warnings)
                            {
                                Debug.Log("  " + warning);
                            }
                        }
                    }

                    return task.Result;
                });
        }

        private DynamicLinkComponents CreateDynamicLinkComponents(Uri uri, string domain,
            DynamicLinkParametersConfiguration parametersConfiguration)
        {
#if UNITY_5_6_OR_NEWER
            string appIdentifier = Application.identifier;
#else
        string appIdentifier = Application.bundleIdentifier;
#endif

            return new DynamicLinkComponents(uri, domain)
            {
                GoogleAnalyticsParameters = parametersConfiguration.GoogleAnalytics,
                IOSParameters = parametersConfiguration.IOs,
                ITunesConnectAnalyticsParameters = parametersConfiguration.ITunesConnectAnalytics,
                AndroidParameters = parametersConfiguration.Android,
                SocialMetaTagParameters = parametersConfiguration.SocialMetaTag
            };
        }
    }
}