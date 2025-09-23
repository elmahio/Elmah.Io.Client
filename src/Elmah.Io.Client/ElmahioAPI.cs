using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Elmah.Io.Client
{
    ///<inheritdoc/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Backwards compatibility")]
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
        public ISourceMapsClient SourceMaps { get; }

        ///<inheritdoc/>
        public IInstallationsClient Installations { get; }

        ///<inheritdoc/>
        public ElmahIoOptions Options { get; }

        /// <summary>
        /// Create a new instance with the specified HttpClient.
        /// </summary>
        protected ElmahioAPI(string baseUrl, ElmahIoOptions options, HttpClient httpClient)
        {
            Options = options;
            Deployments = new DeploymentsClient(httpClient)
            {
                BaseUrl = baseUrl
            };
            Heartbeats = new HeartbeatsClient(httpClient)
            {
                BaseUrl = baseUrl
            };
            Logs = new LogsClient(httpClient)
            {
                BaseUrl = baseUrl
            };
            Messages = new MessagesClient(httpClient, options)
            {
                BaseUrl = baseUrl
            };
            SourceMaps = new SourceMapsClient(httpClient)
            {
                BaseUrl = baseUrl
            };
            Installations = new InstallationsClient(httpClient)
            {
                BaseUrl = baseUrl
            };
        }

        /// <summary>
        /// Create a new instance of the client using the provided HttpClient. The provided HttpClient will be updated
        /// with the API key header and a user agent.
        /// The instance should be shared or kept as a singleton. When bringing your own HttpClient the proxy
        /// and timeout settings on the ElmahIoOptions object will be ignored. The HttpClient should be configured to
        /// use the proxy manually before provided to this method.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "We don't want to allow other base addresses for now.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1510", Justification = "ThrowIfNull not available in old .NET versions.")]
        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options, HttpClient httpClient)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            options ??= new ElmahIoOptions();
            httpClient.DefaultRequestHeaders.Add("api_key", apiKey);
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent(options));
            var client = new ElmahioAPI(httpClient.BaseAddress?.ToString() ?? "https://v4.api.elmah.io/", options, httpClient);
            return client;
        }

        /// <summary>
        /// Create a new instance of the client. The instance should be shared or kept as a singleton.
        /// </summary>
        public static IElmahioAPI Create(string apiKey, ElmahIoOptions options)
        {
            var clientHandler = HttpClientHandlerFactory.GetHttpClientHandler(options);
            var httpClient = new HttpClient(clientHandler)
            {
                Timeout = options.Timeout
            };
            return Create(apiKey, options, httpClient);
        }

        /// <summary>
        /// Create a new instance of the client with default options. The instance should be shared or kept as a singleton.
        /// </summary>
        public static IElmahioAPI Create(string apiKey)
        {
            return Create(apiKey, new ElmahIoOptions());
        }

        private static string UserAgent(ElmahIoOptions options)
        {
            var sb = new StringBuilder();
            sb.Append(new ProductInfoHeaderValue(new ProductHeaderValue("Elmah.Io.Client", $"{typeof(ElmahioAPI).GetTypeInfo().Assembly.GetName().Version}")).ToString());
            if (!string.IsNullOrWhiteSpace(options.UserAgent))
            {
                sb.Append(' ').Append(options.UserAgent);
            }

            return sb.ToString();
        }
    }
}
