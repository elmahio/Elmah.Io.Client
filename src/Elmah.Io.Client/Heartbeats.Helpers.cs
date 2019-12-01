using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial class Heartbeats
    {
        public void Healthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Healthy",
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        public async Task HealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null)
        {
            await this.CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Healthy",
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        public void Degraded(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Degraded",
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        public async Task DegradedAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null)
        {
            await this.CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Degraded",
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        public void Unhealthy(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null)
        {
            this.Create(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Unhealthy",
                Reason = reason,
                Application = application,
                Version = version,
            });
        }

        public async Task UnhealthyAsync(Guid logId, string heartbeatId, string reason = null, string application = null, string version = null)
        {
            await this.CreateAsync(heartbeatId, logId.ToString(), new Models.CreateHeartbeat
            {
                Result = "Unhealthy",
                Reason = reason,
                Application = application,
                Version = version,
            });
        }
    }
}
