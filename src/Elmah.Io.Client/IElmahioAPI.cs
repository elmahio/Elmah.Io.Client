using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Elmah.Io.Client
{
    /// <summary>
    /// A single type to make it easy to keep all clients in a single location.
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
