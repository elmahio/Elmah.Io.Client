using System;
using System.Net.Http;

namespace Elmah.Io.Client
{
    /// <summary>
    /// A single type to make it easy to keep all clients in a single location.
    /// </summary>
    public interface IElmahioAPI
    {
        /// <summary>
        /// Gets the IDeployments.
        /// </summary>
        IDeploymentsClient Deployments { get; }

        /// <summary>
        /// Gets the IHeartbeats.
        /// </summary>
        IHeartbeatsClient Heartbeats { get; }

        /// <summary>
        /// Gets the ILogs.
        /// </summary>
        ILogsClient Logs { get; }

        /// <summary>
        /// Gets the IMessages.
        /// </summary>
        IMessagesClient Messages { get; }

        /// <summary>
        /// Gets the IUptimeChecks.
        /// </summary>
        IUptimeChecksClient UptimeChecks { get; }

        /// <summary>
        /// Gets the ISourceMapsClient.
        /// </summary>
        ISourceMapsClient SourceMaps { get; }

        /// <summary>
        /// The HttpClient used to communicate with the elmah.io API.
        /// </summary>
        [Obsolete("The internal HttpClient used within the elmah.io client will be removed in a future version. You can provide a custom HttpClient through the ElmahioAPI.Create method or you can customize things like timeout and proxy through the ElmahIoOptions class.")]
        HttpClient HttpClient { get; }

        /// <summary>
        /// The options to use for this client.
        /// </summary>
        ElmahIoOptions Options { get; }
    }
}
