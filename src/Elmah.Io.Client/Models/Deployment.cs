// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Elmah.Io.Client.Models
{
    /// <summary> The Deployment. </summary>
    public partial class Deployment
    {
        /// <summary> Initializes a new instance of Deployment. </summary>
        internal Deployment()
        {
        }

        /// <summary> Initializes a new instance of Deployment. </summary>
        /// <param name="id"> The ID of this deployment. </param>
        /// <param name="version">
        /// The version number of this deployment. The value of version can be a SemVer compliant string or any other
        /// 
        /// syntax that you are using as your version numbering scheme.
        /// </param>
        /// <param name="created"> When was this deployment created. </param>
        /// <param name="createdBy">
        /// The elmah.io id of the user creating this deployment. Since deployments are created on a subscription,
        /// 
        /// the CreatedBy will contain the id of the user with the subscription.
        /// </param>
        /// <param name="description"> Sescription of this deployment in markdown or clear text. </param>
        /// <param name="userName"> The name of the person responsible for creating this deployment. </param>
        /// <param name="userEmail"> The email of the person responsible for creating this deployment. </param>
        /// <param name="logId">
        /// If the deployment is attached a single log, this property is set to the ID of that log.
        /// 
        /// If null, the deployment is attached all logs on the organization.
        /// </param>
        internal Deployment(string id, string version, DateTimeOffset? created, string createdBy, string description, string userName, string userEmail, string logId)
        {
            Id = id;
            Version = version;
            Created = created;
            CreatedBy = createdBy;
            Description = description;
            UserName = userName;
            UserEmail = userEmail;
            LogId = logId;
        }

        /// <summary> The ID of this deployment. </summary>
        public string Id { get; }
        /// <summary>
        /// The version number of this deployment. The value of version can be a SemVer compliant string or any other
        /// 
        /// syntax that you are using as your version numbering scheme.
        /// </summary>
        public string Version { get; }
        /// <summary> When was this deployment created. </summary>
        public DateTimeOffset? Created { get; }
        /// <summary>
        /// The elmah.io id of the user creating this deployment. Since deployments are created on a subscription,
        /// 
        /// the CreatedBy will contain the id of the user with the subscription.
        /// </summary>
        public string CreatedBy { get; }
        /// <summary> Sescription of this deployment in markdown or clear text. </summary>
        public string Description { get; }
        /// <summary> The name of the person responsible for creating this deployment. </summary>
        public string UserName { get; }
        /// <summary> The email of the person responsible for creating this deployment. </summary>
        public string UserEmail { get; }
        /// <summary>
        /// If the deployment is attached a single log, this property is set to the ID of that log.
        /// 
        /// If null, the deployment is attached all logs on the organization.
        /// </summary>
        public string LogId { get; }
    }
}
