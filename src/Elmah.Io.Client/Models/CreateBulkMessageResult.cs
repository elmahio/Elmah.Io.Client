// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Elmah.Io.Client.Models
{
    /// <summary> The CreateBulkMessageResult. </summary>
    public partial class CreateBulkMessageResult
    {
        /// <summary> Initializes a new instance of CreateBulkMessageResult. </summary>
        internal CreateBulkMessageResult()
        {
        }

        /// <summary> Initializes a new instance of CreateBulkMessageResult. </summary>
        /// <param name="statusCode">
        /// Status code of the individual messages as if it were being created through the non-bulk endpoint.
        /// 
        /// If a message was succesfully created, the status code will be 201 and location will contain an URL.
        /// 
        /// If a message was ignored, the status code will be 200 and the location will be empty.
        /// </param>
        /// <param name="location"> The location of the created message if StatusCode equals Created. </param>
        internal CreateBulkMessageResult(int? statusCode, string location)
        {
            StatusCode = statusCode;
            Location = location;
        }

        /// <summary>
        /// Status code of the individual messages as if it were being created through the non-bulk endpoint.
        /// 
        /// If a message was succesfully created, the status code will be 201 and location will contain an URL.
        /// 
        /// If a message was ignored, the status code will be 200 and the location will be empty.
        /// </summary>
        public int? StatusCode { get; }
        /// <summary> The location of the created message if StatusCode equals Created. </summary>
        public string Location { get; }
    }
}
