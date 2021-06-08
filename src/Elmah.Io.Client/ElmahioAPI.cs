using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Elmah.Io.Client
{
    ///<inheritdoc/>
    public partial class ElmahioAPI : IElmahioAPI
    {
        ///<inheritdoc/>
        public System.Uri BaseUri { get; set; }

        ///<inheritdoc/>
        public Func<JsonSerializerSettings> SerializationSettings { get; private set; }

        ///<inheritdoc/>
        public IDeploymentsClient Deployments { get; private set; }

        ///<inheritdoc/>
        public IHeartbeatsClient Heartbeats { get; private set; }

        ///<inheritdoc/>
        public ILogsClient Logs { get; private set; }

        ///<inheritdoc/>
        public IMessagesClient Messages { get; private set; }

        ///<inheritdoc/>
        public IUptimeChecksClient UptimeChecks { get; private set; }

        ///<inheritdoc/>
        public HttpClient HttpClient { get; set; }

        /// <summary>
        /// Create a new instance with the specified HttpClient.
        /// </summary>
        protected ElmahioAPI(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
            Initialize();
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        ///</summary>
        partial void CustomInitialize();

        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            BaseUri = new System.Uri("https://api.elmah.io");
            SerializationSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                // ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                {
                    // new Iso8601TimeSpanConverter()
                }
            };
            CustomInitialize();
            Deployments = new DeploymentsClient(this);
            Heartbeats = new HeartbeatsClient(this);
            Logs = new LogsClient(this);
            Messages = new MessagesClient(this);
            UptimeChecks = new UptimeChecksClient(this);
        }
    }

    partial class DeploymentsClient
    {
        public DeploymentsClient(IElmahioAPI elmahioAPI)
        {
            BaseUrl = elmahioAPI.BaseUri.ToString();
            _httpClient = elmahioAPI.HttpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(elmahioAPI.SerializationSettings);
        }
    }

    partial class HeartbeatsClient
    {
        public HeartbeatsClient(IElmahioAPI elmahioAPI)
        {
            BaseUrl = elmahioAPI.BaseUri.ToString();
            _httpClient = elmahioAPI.HttpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(elmahioAPI.SerializationSettings);
        }
    }

    partial class LogsClient
    {
        public LogsClient(IElmahioAPI elmahioAPI)
        {
            BaseUrl = elmahioAPI.BaseUri.ToString();
            _httpClient = elmahioAPI.HttpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(elmahioAPI.SerializationSettings);
        }
    }

    partial class MessagesClient
    {
        public MessagesClient(IElmahioAPI elmahioAPI)
        {
            BaseUrl = elmahioAPI.BaseUri.ToString();
            Options = elmahioAPI.Options;
            _httpClient = elmahioAPI.HttpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(elmahioAPI.SerializationSettings);
        }

        public ElmahIoOptions Options { get; set; }
    }

    partial class UptimeChecksClient
    {
        public UptimeChecksClient(IElmahioAPI elmahioAPI)
        {
            BaseUrl = elmahioAPI.BaseUri.ToString();
            _httpClient = elmahioAPI.HttpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(elmahioAPI.SerializationSettings);
        }
    }
}
