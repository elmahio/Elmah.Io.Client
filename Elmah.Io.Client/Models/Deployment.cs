// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Elmah.Io.Client.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Deployment
    {
        /// <summary>
        /// Initializes a new instance of the Deployment class.
        /// </summary>
        public Deployment() { }

        /// <summary>
        /// Initializes a new instance of the Deployment class.
        /// </summary>
        public Deployment(string id = default(string), string version = default(string), DateTime? created = default(DateTime?), string createdBy = default(string), string description = default(string), string userName = default(string), string userEmail = default(string))
        {
            Id = id;
            Version = version;
            Created = created;
            CreatedBy = createdBy;
            Description = description;
            UserName = userName;
            UserEmail = userEmail;
        }

        /// <summary>
        /// The ID of this deployment.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The version number of this deployment. The value of version can be
        /// a SemVer compliant string or any other
        /// syntax that you are using as your version numbering
        /// scheme.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        /// <summary>
        /// When was this deployment created.
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// The elmah.io id of the user creating this deployment. Since
        /// deployments are created on a subscription,
        /// the CreatedBy will contain the id of the user with the
        /// subscription.
        /// </summary>
        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Sescription of this deployment in markdown or clear text.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The name of the person responsible for creating this deployment.
        /// </summary>
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// The email of the person responsible for creating this deployment.
        /// </summary>
        [JsonProperty(PropertyName = "userEmail")]
        public string UserEmail { get; set; }

    }
}
