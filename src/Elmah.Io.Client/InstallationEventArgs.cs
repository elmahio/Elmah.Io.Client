using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Event args used to signal an installation being created on elmah.io through the OnInstallation event.
    /// </summary>
    /// <remarks>
    /// Create a new instance of the InstallationEventArgs class. This is typically only called from within the elmah.io client.
    /// </remarks>
    /// <param name="installation">The created installation</param>
    public class InstallationEventArgs(CreateInstallation installation) : EventArgs
    {
        /// <summary>
        /// The created installation.
        /// </summary>
        public CreateInstallation Installation { get; set; } = installation;
    }
}
