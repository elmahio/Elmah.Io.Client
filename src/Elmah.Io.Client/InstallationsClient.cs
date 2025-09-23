using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    /// <inheritdoc/>
    public partial class InstallationsClient : IInstallationsClient
    {
        static partial void UpdateJsonSerializerSettings(JsonSerializerOptions settings)
        {
            settings.WriteIndented = true;
#if !NET462
            settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
#endif
            settings.Converters.Add(new JsonStringEnumConverter());
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
