namespace Firebase.Sample.DynamicLinksFolder
{
    public interface IDeepLinkingController
    {
        void Init(IDeepLinkListener listener, string domain);
    }
}