// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Threading;

namespace Firebase.Sample.DynamicLinksFolder {
  using Firebase;
  using Firebase.DynamicLinks;
  using Firebase.Extensions;
  using System;
  using System.Collections;
  using System.Threading.Tasks;
  using UnityEngine;
  using UnityEngine.UI;

  // Handler for UI buttons on the scene.  Also performs some
  // necessary setup (initializing the firebase app, etc) on
  // startup.
  public class UIHandler {

    public GUISkin fb_GUISkin;
    bool UIEnabled = true;
    private string logText = "";
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    const string kInvalidDynamicLinksDomain = "THIS_IS_AN_INVALID_DOMAIN";
    const string kDynamicLinksDomainInvalidError =
      "kDynamicLinksDomain is not valid, link shortening will fail.\n" +
      "To resolve this:\n" +
      "* Goto the Firebase console https://firebase.google.com/console/\n" +
      "* Click on the Dynamic Links tab\n" +
      "* Copy the domain e.g x20yz.app.goo.gl\n" +
      "* Replace the value of kDynamicLinksDomain with the copied domain.\n";
    public string kDynamicLinksDomain;

    // IMPORTANT: You need to set this to a valid domain from the Firebase
    // console (see kDynamicLinksDomainInvalidError for the details).

    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    public virtual void Start() {
      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
        dependencyStatus = task.Result;
        if (dependencyStatus == DependencyStatus.Available) {
          InitializeFirebase();
        } else {
          Debug.LogError(
            "Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
      });
    }
    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase() {
      DynamicLinks.DynamicLinkReceived += OnDynamicLink;
    }

    void OnDestroy() {
      DynamicLinks.DynamicLinkReceived -= OnDynamicLink;
    }

    // Display the dynamic link received by the application.
    void OnDynamicLink(object sender, EventArgs args) {
      var dynamicLinkEventArgs = args as ReceivedDynamicLinkEventArgs;
      Debug.Log(String.Format("Received dynamic link {0}",
                             dynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString));
    }

    DynamicLinkComponents CreateDynamicLinkComponents() {
#if UNITY_5_6_OR_NEWER
      string appIdentifier = Application.identifier;
#else
      string appIdentifier = Application.bundleIdentifier;
#endif

      return new DynamicLinkComponents(
        // The base Link.
        new System.Uri("https://google.com/abc"),
        // The dynamic link domain.
        kDynamicLinksDomain) {
        GoogleAnalyticsParameters = new Firebase.DynamicLinks.GoogleAnalyticsParameters() {
          Source = "mysource",
          Medium = "mymedium",
          Campaign = "mycampaign",
          Term = "myterm",
          Content = "mycontent"
        },
        IOSParameters = new Firebase.DynamicLinks.IOSParameters(appIdentifier) {
          FallbackUrl = new System.Uri("https://mysite/fallback"),
          CustomScheme = "mycustomscheme",
          MinimumVersion = "1.2.3",
          IPadBundleId = appIdentifier,
          IPadFallbackUrl = new System.Uri("https://mysite/fallbackipad")
        },
        ITunesConnectAnalyticsParameters =
          new Firebase.DynamicLinks.ITunesConnectAnalyticsParameters() {
            AffiliateToken = "abcdefg",
            CampaignToken = "hijklmno",
            ProviderToken = "pq-rstuv"
          },
        AndroidParameters = new Firebase.DynamicLinks.AndroidParameters(appIdentifier) {
          FallbackUrl = new System.Uri("https://mysite/fallback"),
          MinimumVersion = 12
        },
        SocialMetaTagParameters = new Firebase.DynamicLinks.SocialMetaTagParameters() {
          Title = "My App!",
          Description = "My app is awesome!",
          ImageUrl = new System.Uri("https://mysite.com/someimage.jpg")
        },
      };
    }

    public Uri CreateAndDisplayLongLink() {
      var longLink = CreateDynamicLinkComponents().LongDynamicLink;
       Debug.Log(String.Format("Long dynamic link {0}", longLink));
      return longLink;
    }

    public Task<ShortDynamicLink> CreateAndDisplayShortLinkAsync() {
      return CreateAndDisplayShortLinkAsync(new DynamicLinkOptions());
    }

    public Task<ShortDynamicLink> CreateAndDisplayUnguessableShortLinkAsync() {
      return CreateAndDisplayShortLinkAsync(new DynamicLinkOptions {
        PathLength = DynamicLinkPathLength.Unguessable
      });
    }

    private Task<ShortDynamicLink> CreateAndDisplayShortLinkAsync(DynamicLinkOptions options) {
      if (kDynamicLinksDomain == kInvalidDynamicLinksDomain) {
         Debug.Log(kDynamicLinksDomainInvalidError);
        var source = new TaskCompletionSource<ShortDynamicLink>();
        source.TrySetException(new Exception(kDynamicLinksDomainInvalidError));
        return source.Task;
      }

      var components = CreateDynamicLinkComponents();
      return DynamicLinks.GetShortLinkAsync(components, options)
        .ContinueWithOnMainThread((task) => {
          if (task.IsCanceled) {
             Debug.Log("Short link creation canceled");
          } else if (task.IsFaulted) {
             Debug.Log(String.Format("Short link creation failed {0}", task.Exception.ToString()));
          } else {
            ShortDynamicLink link = task.Result;
             Debug.Log(String.Format("Generated short link {0}", link.Url));
            var warnings = new System.Collections.Generic.List<string>(link.Warnings);
            if (warnings.Count > 0) {
               Debug.Log("Warnings:");
              foreach (var warning in warnings) {
                 Debug.Log("  " + warning);
              }
            }
          }
          return task.Result;
        });
    }
  }
}

