﻿using System;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial class Heartbeats
    {
        /// <inheritdoc/>
        public void Healthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null, long? took = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Healthy",
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
                    Result = "Healthy",
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
                Result = "Degraded",
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
                    Result = "Degraded",
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
                Result = "Unhealthy",
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
                    Result = "Unhealthy",
                    Reason = reason,
                    Application = application,
                    Version = version,
                    Took = took,
                })
                .ConfigureAwait(false);
        }
    }
}
