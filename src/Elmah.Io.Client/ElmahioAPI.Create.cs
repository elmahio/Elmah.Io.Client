namespace Elmah.Io.Client
{
    public partial class ElmahioAPI
    {
        public ElmahIoOptions Options { get; set; } = new ElmahIoOptions();

        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            return new ElmahioAPI(new ApiKeyCredentials(apiKey))
            {
                Options = options ?? new ElmahIoOptions()
            };
        }

        public static IElmahioAPI Create(string apiKey)
        {
            return Create(apiKey, new ElmahIoOptions());
        }
    }
}