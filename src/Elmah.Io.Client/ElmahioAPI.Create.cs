using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Elmah.Io.Client
{
    public partial class ElmahioAPI
    {
        public HttpClient HttpClient { get; set; } = new HttpClient();
        public ElmahIoOptions Options { get; set; } = new ElmahIoOptions();

        public static ElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            var client = new ElmahioAPI()
            {
                Options = options
            };
            var clientHandler = HttpClientHandlerFactory.GetHttpClientHandler(options);
            client.HttpClient = new HttpClient(clientHandler);
            client.HttpClient.DefaultRequestHeaders.Add("api_key", apiKey);
            client.HttpClient.Timeout = new TimeSpan(0, 0, 5);
            client.HttpClient.DefaultRequestHeaders.UserAgent.Clear();
            client.HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Elmah.Io.Client", $"{typeof(ElmahioAPI).GetTypeInfo().Assembly.GetName().Version}")));
            return client;
        }

        public static ElmahioAPI Create(string apiKey)
        {
            return Create(apiKey, new ElmahIoOptions());
        }
    }
}