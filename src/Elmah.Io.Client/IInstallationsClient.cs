using System;
using System.Threading.Tasks;
using System.Threading;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Interface for methods creating installations.
    /// </summary>
    public partial interface IInstallationsClient
    {
        /// <summary>
        /// By subscribing to the OnInstallation event, you can hook into the pipeline of creating installations to elmah.io.
        /// The event is triggered just before calling elmah.io's API.
        /// </summary>
        event EventHandler<InstallationEventArgs> OnInstallation;

        /// <summary>
        /// Low level create installation method, which all other methods wanting to create an installation should ideally call.
        /// The CreateAndNotify method triggers the OnInstallation event.
        /// </summary>
        void CreateAndNotify(Guid logId, CreateInstallation installation);

        /// <summary>
        /// Low level create installation method, which all other methods wanting to create an installation should ideally call.
        /// The CreateAndNotifyAsync method triggers the OnInstallation event.
        /// </summary>
        Task CreateAndNotifyAsync(Guid logId, CreateInstallation installation, CancellationToken cancellationToken = default);
    }
}
