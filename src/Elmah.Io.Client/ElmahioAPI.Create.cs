using System;
using System.Net.Http.Headers;
using System.Reflection;

namespace Elmah.Io.Client
{
    public partial class ElmahioAPI
    {
        public ElmahIoOptions Options { get; set; } = new ElmahIoOptions();

        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            options = options ?? new ElmahIoOptions();
            var client = new ElmahioAPI(new ApiKeyCredentials(apiKey), HttpClientHandlerFactory.GetHttpClientHandler(options))
            {
                Options = options
            };
            client.HttpClient.Timeout = new TimeSpan(0, 0, 5);
            client.HttpClient.DefaultRequestHeaders.UserAgent.Clear();
            client.HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Elmah.Io.Client", $"{typeof(ElmahioAPI).GetTypeInfo().Assembly.GetName().Version}")));
            return client;
        }

        public static IElmahioAPI Create(string apiKey)
        {
            return Create(apiKey, new ElmahIoOptions());
        }
    }
}