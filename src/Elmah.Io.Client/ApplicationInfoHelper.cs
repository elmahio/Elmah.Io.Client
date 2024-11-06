using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Elmah.Io.Client
{
    /// <summary>
    /// A class that can provide a guess of the current application type. The class requires either >= .NET 4.5 or >= NETSTANDARD 2.0.
    /// </summary>
    public static class ApplicationInfoHelper
    {
        /// <summary>
        /// Get the best guess of the current application type.
        /// </summary>
        /// <returns>Null or one of these values: aspnet, aspnetcore, console, azurefunction, service, windowsapp</returns>
        public static string GetApplicationType()
        {
#if NETSTANDARD2_0_OR_GREATER || NET45_OR_GREATER || NETCOREAPP2_0_OR_GREATER
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var processName = Process.GetCurrentProcess().ProcessName;
            if (assemblies.Exists(a => a.FullName.StartsWith("System.Web")) && (processName == "w3wp" || processName == "iisexpress"))
                return "aspnet";
            else if (assemblies.Exists(a => a.FullName.StartsWith("Microsoft.Azure.Functions") || a.FullName.StartsWith("Microsoft.Azure.WebJobs")))
                return "azurefunction";
            else if (assemblies.Exists(a => a.FullName.StartsWith("Microsoft.AspNetCore")))
                return "aspnetcore";
            else if (assemblies.Exists(a => a.FullName.StartsWith("System.Windows.Forms") || a.FullName.StartsWith("PresentationCore") || a.FullName.StartsWith("WindowsBase")))
                return "windowsapp";
            else if (Console.OpenStandardInput() != Stream.Null)
                return "console";
            else if (!Environment.UserInteractive)
                return "service";
#endif

            return null;
        }
    }
}
