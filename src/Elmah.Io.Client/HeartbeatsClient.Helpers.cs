using System;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial class HeartbeatsClient
    {
        private const string DegradedResult = "Degraded";
        private const string HealthyResult = "Healthy";
        private const string UnhealthyResult = "Unhealthy";

        /// <summary>
        /// Helper for running a piece of code and automatically logging a heartbeat. If the code return true, a healthy heartbeat is logged.
        /// If the code return false or throw an exception, an unhealthy heartbeat is logged.
        /// </summary>
        public void Check(Func<bool> func, Guid logId, string heartbeatId, string application = null, string version = null)
        {
            var result = HealthyResult;
            string reason = null;
            try
            {
                if (!func()) result = UnhealthyResult;
            }
            catch (Exception e)
            {
                result = UnhealthyResult;
                reason = e.ToString();
            }

            Create(heartbeatId, logId.ToString(), new CreateHeartbeat
            {
                Result = result,
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        /// <summary>
        /// Helper for running a piece of code and automatically logging a heartbeat. If the code return true, a healthy heartbeat is logged.
        /// If the code return false or throw an exception, an unhealthy heartbeat is logged.
        /// </summary>
        public async Task CheckAsync(Func<Task<bool>> func, Guid logId, string heartbeatId, string application = null, string version = null)
        {
            var result = HealthyResult;
            string reason = null;
            try
            {
                if (!await func().ConfigureAwait(false)) result = UnhealthyResult;
            }
            catch (Exception e)
            {
                result = UnhealthyResult;
                reason = e.ToString();
            }

            await
                CreateAsync(heartbeatId, logId.ToString(), new CreateHeartbeat
                {
                    Result = result,
                    Reason = reason,
                    Application = application,
                    Version = version,
                })
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Logs a healthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        public void Healthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            Create(heartbeatId, logId.ToString(), new CreateHeartbeat
            {
                Result = HealthyResult,
                Reason = reason,
                Application = application,
                Version = version,
                Took = took,
            });
        }

        /// <summary>
        /// Logs a healthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        public async Task HealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            await
                CreateAsync(heartbeatId, logId.ToString(), new CreateHeartbeat
                {
                    Result = HealthyResult,
                    Reason = reason,
                    Application = application,
                    Version = version,
                    Took = took,
                })
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Logs a degraded heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        public void Degraded(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            Create(heartbeatId, logId.ToString(), new CreateHeartbeat
            {
                Result = DegradedResult,
                Reason = reason,
                Application = application,
                Version = version,
                Took = took,
            });
        }

        /// <summary>
        /// Logs a degraded heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        public async Task DegradedAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            await
                CreateAsync(heartbeatId, logId.ToString(), new CreateHeartbeat
                {
                    Result = DegradedResult,
                    Reason = reason,
                    Application = application,
                    Version = version,
                    Took = took,
                })
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Logs a unhealthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        public void Unhealthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            Create(heartbeatId, logId.ToString(), new CreateHeartbeat
            {
                Result = UnhealthyResult,
                Reason = reason,
                Application = application,
                Version = version,
                Took = took,
            });
        }

        /// <summary>
        /// Logs a unhealthy heartbeat in the specified log on the specified heartbeat.
        /// </summary>
        public async Task UnhealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            await
                CreateAsync(heartbeatId, logId.ToString(), new CreateHeartbeat
                {
                    Result = UnhealthyResult,
                    Reason = reason,
                    Application = application,
                    Version = version,
                    Took = took,
                })
                .ConfigureAwait(false);
        }
    }
}
