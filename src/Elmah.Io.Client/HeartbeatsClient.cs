﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    partial class HeartbeatsClient : IHeartbeatsClient
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

            Create(heartbeatId, logId.ToString(), new CreateHeartbeat
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.Converters = new List<JsonConverter>();
        }
    }
}
