using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Extension methods for logging assembly information in a fail-safe way, ensuring that missing assemblies do not cause issues in restricted environments.
    /// </summary>
    public static class AssemblyLoggerExtensions
    {
        /// <summary>
        /// Add assembly information to the collection if the assembly is loaded and accessible. This method safely handles cases
        /// where the assembly may not be present or accessible due to environment restrictions, such as in Blazor WebAssembly.
        /// </summary>
        public static void TryAddAssemblyIfLoaded(this ICollection<AssemblyInfo> assemblies, string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) return;

            try
            {
                var name = new AssemblyName(assemblyName);
                var assembly = Assembly.Load(name);

                if (assembly != null)
                {
                    var versionAttribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
                    var version = versionAttribute?.Version
                        ?? name.Version?.ToString()
                        ?? "1.0.0";

                    assemblies.Add(new AssemblyInfo
                    {
                        Name = assemblyName,
                        Version = version
                    });
                }
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is BadImageFormatException)
            {
                // The assembly is not present in the bin folder, ignore it safely
            }
            catch
            {
                // Fail-safe for any other environment restrictions
            }
        }

        /// <summary>
        /// Add known Elmah.Io.Client.Extensions assemblies to the collection if they are loaded. This method ensures that
        /// we capture extension assemblies without causing issues in environments where they may not be present.
        /// </summary>
        public static void TryAddExtensionAssembliesIfLoaded(this ICollection<AssemblyInfo> assemblies)
        {
            // Add any future Elmah.Io.Client.Extensions.* packages straight to this array
            var extensionNames = new[]
            {
                "Elmah.Io.Client.Extensions.SourceCode",
                "Elmah.Io.Client.Extensions.Correlation"
            };

            foreach (var name in extensionNames)
            {
                assemblies.TryAddAssemblyIfLoaded(name);
            }
        }
    }
}