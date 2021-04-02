using Azure.Core;
using Azure.Core.Pipeline;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Elmah.Io.Client
{
    public partial class ElmahioAPI
    {
        public ElmahIoOptions Options { get; set; } = new ElmahIoOptions();
        public HttpClient HttpClient { get; private set; }

        public static ElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            ClientOptions clientOptions = new ElmahioDiagnosticClientOptions();
            ClientDiagnostics clientDiagnostics = new ClientDiagnostics(clientOptions);

            var httpClient = new ApiKeyCredentials(apiKey);

            HttpPipeline httpPipeline = new HttpPipeline(new HttpClientTransport(httpClient));

            options ??= new ElmahIoOptions();
            var client = new ElmahioAPI(clientDiagnostics, httpPipeline, null)
            {
                Options = options
            };
            client.HttpClient = httpClient;
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