// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Elmah.Io.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Log
    {
        /// <summary>
        /// Initializes a new instance of the Log class.
        /// </summary>
        public Log()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Log class.
        /// </summary>
        public Log(string id = default(string), string name = default(string))
        {
            Id = id;
            Name = name;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

    }
}