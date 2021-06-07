using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Elmah.Io.Client
{
    /// <summary>
    /// This is the public REST API for elmah.io. All of the integrations
    /// communicates with elmah.io through this API.&lt;br/&gt;&lt;br/&gt;For
    /// additional help getting started with the API, visit the following help
    /// articles:&lt;br/&gt;&lt;ul&gt;&lt;li&gt;[Using the REST
    /// API](https://docs.elmah.io/using-the-rest-api/)&lt;/li&gt;&lt;li&gt;[Where
    /// is my API
    /// key?](https://docs.elmah.io/where-is-my-api-key/)&lt;/li&gt;&lt;li&gt;[Where
    /// is my log
    /// ID?](https://docs.elmah.io/where-is-my-log-id/)&lt;/li&gt;&lt;li&gt;[How
    /// to configure API key
    /// permissions](https://docs.elmah.io/how-to-configure-api-key-permissions/)&lt;/li&gt;&lt;/ul&gt;
    /// </summary>
    public partial interface IElmahioAPI
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        Func<JsonSerializerSettings> SerializationSettings { get; }

        /// <summary>
        /// Gets the IDeployments.
        /// </summary>
        DeploymentsClient Deployments { get; }

        /// <summary>
        /// Gets the IHeartbeats.
        /// </summary>
        HeartbeatsClient Heartbeats { get; }

        /// <summary>
        /// Gets the ILogs.
        /// </summary>
        LogsClient Logs { get; }

        /// <summary>
        /// Gets the IMessages.
        /// </summary>
        MessagesClient Messages { get; }

        /// <summary>
        /// Gets the IUptimeChecks.
        /// </summary>
        UptimeChecksClient UptimeChecks { get; }

        /// <summary>
        /// Gets the HttpClient used in every client.
        /// </summary>
        HttpClient HttpClient { get; set; }
    }
}
