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
        /// The options to use for this client.
        /// </summary>
        ElmahIoOptions Options { get; }
    }
}
