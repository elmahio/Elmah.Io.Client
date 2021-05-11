using System;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial interface IHeartbeats
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
        Task CheckAsync(Func<Task<bool>> func, Guid logId, string heartbeatId, string application = null, string version = null);
        /// <summary>
        /// Logs a healthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        void Healthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null);
        /// <summary>   
        /// Logs a healthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        Task HealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null);
        /// <summary>
        /// Logs a degraded heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        void Degraded(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null);
        /// <summary>
        /// Logs a degraded heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        Task DegradedAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null);
        /// <summary>
        /// Logs a unhealthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        void Unhealthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null);
        /// <summary>
        /// Logs a unhealthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        Task UnhealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null);
    }
}
