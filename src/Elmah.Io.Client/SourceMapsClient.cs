using Newtonsoft.Json;

namespace Elmah.Io.Client
{
    /// <inheritdoc/>
    public partial class SourceMapsClient
    {
        static partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.Converters = [];
        }
    }
}
