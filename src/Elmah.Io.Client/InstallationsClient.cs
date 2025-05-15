using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Elmah.Io.Client
{
    /// <inheritdoc/>
    public partial class InstallationsClient : IInstallationsClient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance rules", "CA1822", Justification = "Method is not static in auto-generated class with this partial method")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "This is needed")]
        static partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.Converters = [];
        }

        /// <inheritdoc/>
        public event EventHandler<InstallationEventArgs> OnInstallation;

        /// <inheritdoc/>
        public void CreateAndNotify(Guid logId, CreateInstallation installation)
        {
            OnInstallation?.Invoke(this, new InstallationEventArgs(installation));
            Create(logId.ToString(), installation);
        }

        /// <inheritdoc/>
        public async Task CreateAndNotifyAsync(Guid logId, CreateInstallation installation, CancellationToken cancellationToken = default)
        {
            OnInstallation?.Invoke(this, new InstallationEventArgs(installation));
            await CreateAsync(logId.ToString(), installation, cancellationToken).ConfigureAwait(false);
        }
    }
}
