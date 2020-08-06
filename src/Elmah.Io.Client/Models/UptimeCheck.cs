// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Elmah.Io.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class UptimeCheck
    {
        /// <summary>
        /// Initializes a new instance of the UptimeCheck class.
        /// </summary>
        public UptimeCheck()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UptimeCheck class.
        /// </summary>
        /// <param name="id">ID of the uptime check.</param>
        /// <param name="name">Name of the uptime check.</param>
        /// <param name="url">Url of the uptime check.</param>
        /// <param name="status">Current status of the uptime check.</param>
        public UptimeCheck(string id = default(string), string name = default(string), string url = default(string), string status = default(string))
        {
            Id = id;
            Name = name;
            Url = url;
            Status = status;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets ID of the uptime check.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name of the uptime check.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets url of the uptime check.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets current status of the uptime check.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}
