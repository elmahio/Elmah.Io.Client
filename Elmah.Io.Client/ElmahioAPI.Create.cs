namespace Elmah.Io.Client
{
    public partial class ElmahioAPI
    {
        public static IElmahioAPI Create(string apiKey)
        {
            return new ElmahioAPI(new ApiKeyCredentials(apiKey));
        }
    }
}