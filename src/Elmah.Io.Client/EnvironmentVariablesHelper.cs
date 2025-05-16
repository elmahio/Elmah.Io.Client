using System;
using System.Collections.Generic;

namespace Elmah.Io.Client
{
    /// <summary>
    /// A class with helper methods for getting environment variables.
    /// </summary>
    public static class EnvironmentVariablesHelper
    {
        /// <summary>
        /// Try to get an environment variable value from one or more names.
        /// </summary>
        public static string? GetEnvironmentVariable(params string[] keys)
        {
#if !NETSTANDARD1_1
            foreach (var key in keys)
            {
                var value = Environment.GetEnvironmentVariable(key);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }
#endif

            return null;
        }

        /// <summary>
        /// Try to get elmah.io specific environment variables based on the format ElmahIo:* and ElmahIo__*.
        /// </summary>
        public static List<Item> GetElmahIoAppSettingsEnvironmentVariables()
        {
            var variables = new List<Item>();
            if (GetEnvironmentVariable("ElmahIo:LogId", "ElmahIo__LogId") is string logIdEnv) variables.Add(new Item("ElmahIo:LogId", logIdEnv));
            if (GetEnvironmentVariable("ElmahIo.ApiKey", "ElmahIo__ApiKey") is string apiKeyEnv) variables.Add(new Item("ElmahIo:ApiKey", apiKeyEnv));

            return variables;
        }

        /// <summary>
        /// Try to get Azure specific environment variables.
        /// </summary>
        public static List<Item> GetAzureEnvironmentVariables()
        {
            var variables = new List<Item>();
            if (GetEnvironmentVariable("WEBSITE_SITE_NAME") is string websiteSiteName) variables.Add(new Item("WEBSITE_SITE_NAME", websiteSiteName));
            if (GetEnvironmentVariable("WEBSITE_RESOURCE_GROUP") is string websiteResourceGroup) variables.Add(new Item("WEBSITE_RESOURCE_GROUP", websiteResourceGroup));
            if (GetEnvironmentVariable("WEBSITE_OWNER_NAME") is string websiteOwnerName) variables.Add(new Item("WEBSITE_OWNER_NAME", websiteOwnerName));
            if (GetEnvironmentVariable("REGION_NAME") is string regionName) variables.Add(new Item("REGION_NAME", regionName));
            if (GetEnvironmentVariable("WEBSITE_SKU") is string websiteSku) variables.Add(new Item("WEBSITE_SKU", websiteSku));

            return variables;
        }

        /// <summary>
        /// Try to get ASP.NET Core specific environment variables.
        /// </summary>
        public static List<Item> GetAspNetCoreEnvironmentVariables()
        {
            var variables = new List<Item>();
            if (GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is string aspNetCoreEnvironment) variables.Add(new Item("ASPNETCORE_ENVIRONMENT", aspNetCoreEnvironment));

            return variables;
        }

        /// <summary>
        /// Try to get .NET specific environment variables.
        /// </summary>
        public static List<Item> GetDotNetEnvironmentVariables()
        {
            var variables = new List<Item>();
            if (GetEnvironmentVariable("DOTNET_ENVIRONMENT") is string dotNetEnvironment) variables.Add(new Item("DOTNET_ENVIRONMENT", dotNetEnvironment));
            if (GetEnvironmentVariable("DOTNET_VERSION") is string dotNetVersion) variables.Add(new Item("DOTNET_VERSION", dotNetVersion));
            if (GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") is string processorArchitecture) variables.Add(new Item("PROCESSOR_ARCHITECTURE", processorArchitecture));

            return variables;
        }

        /// <summary>
        /// Try to get Azure Functions specific environment variables.
        /// </summary>
        public static List<Item> TryGetAzureFunctionsEnvironmentVariables()
        {
            var variables = new List<Item>();
            if (GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") is string azureFunctionsEnvironment) variables.Add(new Item("AZURE_FUNCTIONS_ENVIRONMENT", azureFunctionsEnvironment));
            if (GetEnvironmentVariable("FUNCTIONS_WORKER_RUNTIME") is string functionsWorkerRuntime) variables.Add(new Item("FUNCTIONS_WORKER_RUNTIME", functionsWorkerRuntime));
            if (GetEnvironmentVariable("FUNCTIONS_EXTENSION_VERSION") is string functionsExtensionVersion) variables.Add(new Item("FUNCTIONS_EXTENSION_VERSION", functionsExtensionVersion));
            if (GetEnvironmentVariable("WEBSITE_SLOT_NAME") is string websiteSlotName) variables.Add(new Item("WEBSITE_SLOT_NAME", websiteSlotName));

            return variables;
        }
    }
}