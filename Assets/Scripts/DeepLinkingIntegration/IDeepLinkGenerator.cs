using System;
using System.Threading.Tasks;
using Firebase.DynamicLinks;

namespace DeepLinkingIntegration
{
    public interface IDeepLinkGenerator
    {
        Uri GenerateLongLink(Uri uriLink, string domain,
            DynamicLinkParametersConfiguration dynamicLinkParametersConfiguration);

        Task<ShortDynamicLink> GenerateShortLink(Uri uri, string domain,
            DynamicLinkParametersConfiguration dynamicLinkParametersConfiguration, DynamicLinkOptions options);
    }
}