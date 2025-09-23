using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elmah.Io.Client
{
    /// <inheritdoc/>
    partial class DeploymentsClient
    {
        static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
            settings.WriteIndented = true;
#if !NET462
            settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
#endif
            settings.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
