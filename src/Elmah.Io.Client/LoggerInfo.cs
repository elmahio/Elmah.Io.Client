using System.Collections.Generic;

namespace Elmah.Io.Client
{
    public partial class LoggerInfo
    {
        /// <summary>
        /// A list of environment variables relevant for this logger.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("environmentVariables", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ICollection<Item> EnvironmentVariables { get; set; }
    }
}
