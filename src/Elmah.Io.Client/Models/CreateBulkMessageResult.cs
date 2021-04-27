// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Elmah.Io.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class CreateBulkMessageResult
    {
        /// <summary>
        /// Initializes a new instance of the CreateBulkMessageResult class.
        /// </summary>
        public CreateBulkMessageResult()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CreateBulkMessageResult class.
        /// </summary>
        /// <param name="statusCode">Status code of the individual messages as
        /// if it were being created through the non-bulk endpoint.
        /// If a message was succesfully created, the status code will be 201
        /// and location will contain an URL.
        /// If a message was ignored, the status code will be 200 and the
        /// location will be empty.</param>
        /// <param name="location">The location of the created message if
        /// StatusCode equals Created.</param>
        public CreateBulkMessageResult(int? statusCode = default(int?), string location = default(string))
        {
            StatusCode = statusCode;
            Location = location;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets status code of the individual messages as if it were
        /// being created through the non-bulk endpoint.
        /// If a message was succesfully created, the status code will be 201
        /// and location will contain an URL.
        /// If a message was ignored, the status code will be 200 and the
        /// location will be empty.
        /// </summary>
        [JsonProperty(PropertyName = "statusCode")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the location of the created message if StatusCode
        /// equals Created.
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

    }
}