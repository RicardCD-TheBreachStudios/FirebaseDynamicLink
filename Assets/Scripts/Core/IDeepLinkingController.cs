namespace Firebase.Sample.DynamicLinksFolder
{
    public interface IDeepLinkingController
    {
        void Init(IDeepLinkLReceiver deepLinkReceiver, string domain);
    }
}