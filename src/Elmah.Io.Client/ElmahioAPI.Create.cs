using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Elmah.Io.Client
{
    public partial class ElmahioAPI
    {
        /// <summary>
        /// The options to use for this client.
        /// </summary>
        public ElmahIoOptions Options { get; set; }

        /// <summary>
        /// Create a new instance of the client. The instance should be shared or kept as a singleton.
        /// </summary>
        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            var clientHandler = HttpClientHandlerFactory.GetHttpClientHandler(options);
            var httpClient = new HttpClient(clientHandler);
            var client = new ElmahioAPI(httpClient)
            {
                Options = options
            };
            client.HttpClient.DefaultRequestHeaders.Add("api_key", apiKey);
            client.HttpClient.Timeout = new TimeSpan(0, 0, 5);
            client.HttpClient.DefaultRequestHeaders.UserAgent.Clear();
            client.HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Elmah.Io.Client", $"{typeof(ElmahioAPI).GetTypeInfo().Assembly.GetName().Version}")));
            return client;
        }

        /// <summary>
        /// Create a new instance of the client with default options. The instance should be shared or kept as a singleton.
        /// </summary>
        public static IElmahioAPI Create(string apiKey)
        {
            return Create(apiKey, new ElmahIoOptions());
        }
    }
}