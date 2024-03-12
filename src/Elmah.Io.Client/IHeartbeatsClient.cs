using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Interface for methods creating heartbeats.
    /// </summary>
    public partial interface IHeartbeatsClient
    {
        /// <summary>
        /// Helper for running a piece of code and automatically logging a heartbeat. If the code return true, a healthy heartbeat is logged.
        /// If the code return false or throw an exception, an unhealthy heartbeat is logged.
        /// </summary>
        void Check(Func<bool> func, Guid logId, string heartbeatId, string application = null, string version = null);

        /// <summary>
        /// Helper for running a piece of code and automatically logging a heartbeat. If the code return true, a healthy heartbeat is logged.
        /// If the code return false or throw an exception, an unhealthy heartbeat is logged.
        /// </summary>
        Task CheckAsync(Func<Task<bool>> func, Guid logId, string heartbeatId, string application = null, string version = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Logs a healthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        void Healthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null, List<Check> checks = null);

        /// <summary>
        /// Logs a healthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        Task HealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null, List<Check> checks = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Logs a degraded heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        void Degraded(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null, List<Check> checks = null);

        /// <summary>
        /// Logs a degraded heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        Task DegradedAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null, List<Check> checks = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Logs a unhealthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        void Unhealthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null, List<Check> checks = null);

        /// <summary>
        /// Logs a unhealthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        Task UnhealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null, List<Check> checks = null, CancellationToken cancellationToken = default);
    }
}
