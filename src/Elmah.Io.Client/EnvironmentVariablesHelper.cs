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
        public static bool TryGetEnvironmentVariable(string[] keys, out string outputValue)
        {
#if !NETSTANDARD1_1
            foreach (var key in keys)
            {
                var value = Environment.GetEnvironmentVariable(key);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    outputValue = value;
                    return true;
                }
            }
#endif

            outputValue = null;
            return false;
        }

        /// <summary>
        /// Try to get elmah.io specific environment variables based on the format ElmahIo:* and ElmahIo__*.
        /// </summary>
        public static bool TryGetElmahIoAppSettingsEnvironmentVariables(out List<Item> variables)
        {
            variables = [];
            if (TryGetEnvironmentVariable(["ElmahIo:LogId", "ElmahIo__LogId"], out string logIdEnv)) variables.Add(new Item("ElmahIo:LogId", logIdEnv));
            if (TryGetEnvironmentVariable(["ElmahIo.ApiKey", "ElmahIo__ApiKey"], out string apiKeyEnv)) variables.Add(new Item("ElmahIo:ApiKey", apiKeyEnv));

            return variables.Count > 0;
        }

        /// <summary>
        /// Try to get Azure specific environment variables.
        /// </summary>
        public static bool TryGetAzureEnvironmentVariables(out List<Item> variables)
        {
            variables = [];
            if (TryGetEnvironmentVariable(["WEBSITE_SITE_NAME"], out string websiteSiteName)) variables.Add(new Item("WEBSITE_SITE_NAME", websiteSiteName));
            if (TryGetEnvironmentVariable(["WEBSITE_RESOURCE_GROUP"], out string websiteResourceGroup)) variables.Add(new Item("WEBSITE_RESOURCE_GROUP", websiteResourceGroup));
            if (TryGetEnvironmentVariable(["WEBSITE_OWNER_NAME"], out string websiteOwnerName)) variables.Add(new Item("WEBSITE_OWNER_NAME", websiteOwnerName));
            if (TryGetEnvironmentVariable(["REGION_NAME"], out string regionName)) variables.Add(new Item("REGION_NAME", regionName));
            if (TryGetEnvironmentVariable(["WEBSITE_SKU"], out string websiteSku)) variables.Add(new Item("WEBSITE_SKU", websiteSku));

            return variables.Count > 0;
        }

        /// <summary>
        /// Try to get ASP.NET Core specific environment variables.
        /// </summary>
        public static bool TryGetAspNetCoreEnvironmentVariables(out List<Item> variables)
        {
            variables = [];
            if (TryGetEnvironmentVariable(["ASPNETCORE_ENVIRONMENT"], out string aspNetCoreEnvironment)) variables.Add(new Item("ASPNETCORE_ENVIRONMENT", aspNetCoreEnvironment));

            return variables.Count > 0;
        }

        /// <summary>
        /// Try to get .NET specific environment variables.
        /// </summary>
        public static bool TryGetDotNetEnvironmentVariables(out List<Item> variables)
        {
            variables = [];
            if (TryGetEnvironmentVariable(["DOTNET_ENVIRONMENT"], out string dotNetEnvironment)) variables.Add(new Item("DOTNET_ENVIRONMENT", dotNetEnvironment));
            if (TryGetEnvironmentVariable(["DOTNET_VERSION"], out string dotNetVersion)) variables.Add(new Item("DOTNET_VERSION", dotNetVersion));
            if (TryGetEnvironmentVariable(["PROCESSOR_ARCHITECTURE"], out string processorArchitecture)) variables.Add(new Item("PROCESSOR_ARCHITECTURE", processorArchitecture));

            return variables.Count > 0;
        }
    }
}
