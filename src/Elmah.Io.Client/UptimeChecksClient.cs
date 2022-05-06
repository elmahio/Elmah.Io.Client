using Newtonsoft.Json;
using System.Collections.Generic;

namespace Elmah.Io.Client
{
    /// <inheritdoc/>
    partial class UptimeChecksClient
    {
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
