using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Elmah.Io.Client
{
    ///<inheritdoc/>
    public class ElmahioAPI : IElmahioAPI
    {
        ///<inheritdoc/>
        public IDeploymentsClient Deployments { get; }

        ///<inheritdoc/>
        public IHeartbeatsClient Heartbeats { get; }

        ///<inheritdoc/> d sa
        public ILogsClient Logs { get; }

        ///<inheritdoc/>
        public IMessagesClient Messages { get; }

        ///<inheritdoc/>
        public IUptimeChecksClient UptimeChecks { get; }

        ///<inheritdoc/>
        public ISourceMapsClient SourceMaps { get; }

        ///<inheritdoc/>
        public HttpClient HttpClient { get; }

        ///<inheritdoc/>
        public ElmahIoOptions Options { get; }

        /// <summary>
        /// Create a new instance with the specified HttpClient.
        /// </summary>
        protected ElmahioAPI(string baseUrl, ElmahIoOptions options, HttpClient httpClient)
        {
            HttpClient = httpClient;
            Options = options;
            Deployments = new DeploymentsClient(baseUrl, httpClient);
            Heartbeats = new HeartbeatsClient(baseUrl, httpClient);
            Logs = new LogsClient(baseUrl, httpClient);
            Messages = new MessagesClient(baseUrl, httpClient, options);
            UptimeChecks = new UptimeChecksClient(baseUrl, httpClient);
            SourceMaps = new SourceMapsClient(baseUrl, httpClient);
        }

        /// <summary>
        /// Create a new instance of the client using the provided HttpClient. The provided HttpClient will be updated
        /// with the API key header, a user agent, and the base URL for the elmah.io API (if not already set).
        /// The instance should be shared or kept as a singleton.
        /// </summary>
        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options, HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            if (options == null) options = new ElmahIoOptions();
            var client = new ElmahioAPI(httpClient.BaseAddress?.ToString() ?? "https://api.elmah.io/", options, httpClient);
            client.HttpClient.DefaultRequestHeaders.Add("api_key", apiKey);
            client.HttpClient.DefaultRequestHeaders.UserAgent.Clear();
            client.HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Elmah.Io.Client", $"{typeof(ElmahioAPI).GetTypeInfo().Assembly.GetName().Version}")));
            return client;
        }

        /// <summary>
        /// Create a new instance of the client. The instance should be shared or kept as a singleton.
        /// </summary>
        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            var clientHandler = HttpClientHandlerFactory.GetHttpClientHandler(options);
            var httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = new TimeSpan(0, 0, 5);
            return Create(apiKey, options, httpClient);
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
