using System;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial class Heartbeats
    {
        private const string DegradedResult = "Degraded";
        private const string HealthyResult = "Healthy";
        private const string UnhealthyResult = "Unhealthy";

        /// <inheritdoc/>
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

            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = result,
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        /// <inheritdoc/>
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

            await this
                .CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
                {
                    Result = result,
                    Reason = reason,
                    Application = application,
                    Version = version,
                })
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Healthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = HealthyResult,
                Reason = reason,
                Application = application,
                Version = version,
                Took = took,
            });
        }

        /// <inheritdoc/>
        public async Task HealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            await this
                .CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
                {
                    Result = HealthyResult,
                    Reason = reason,
                    Application = application,
                    Version = version,
                    Took = took,
                })
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Degraded(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = DegradedResult,
                Reason = reason,
                Application = application,
                Version = version,
                Took = took,
            });
        }

        /// <inheritdoc/>
        public async Task DegradedAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            await this
                .CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
                {
                    Result = DegradedResult,
                    Reason = reason,
                    Application = application,
                    Version = version,
                    Took = took,
                })
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Unhealthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = UnhealthyResult,
                Reason = reason,
                Application = application,
                Version = version,
                Took = took,
            });
        }

        /// <inheritdoc/>
        public async Task UnhealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            await this
                .CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
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
