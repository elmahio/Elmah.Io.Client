// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using Azure.Core;

namespace Elmah.Io.Client.Models
{
    /// <summary> The Message. </summary>
    public partial class Message
    {
        /// <summary> Initializes a new instance of Message. </summary>
        internal Message()
        {
            Cookies = new ChangeTrackingList<Item>();
            Form = new ChangeTrackingList<Item>();
            QueryString = new ChangeTrackingList<Item>();
            ServerVariables = new ChangeTrackingList<Item>();
            Data = new ChangeTrackingList<Item>();
        }

        /// <summary> Initializes a new instance of Message. </summary>
        /// <param name="id"> The ID of this message. </param>
        /// <param name="application"> Used to identify which application logged this message. You can use this if you have multiple applications and services logging to the same log. </param>
        /// <param name="detail"> A longer description of the message. For errors this could be a stacktrace, but it&apos;s really up to you what to log in there. </param>
        /// <param name="hostname"> The hostname of the server logging the message. </param>
        /// <param name="title"> The textual title or headline of the message to log. </param>
        /// <param name="titleTemplate">
        /// The title template of the message to log. This property can be used from logging frameworks that supports
        /// 
        /// structured logging like: &quot;{user} says {quote}&quot;. In the example, titleTemplate will be this string and title
        /// 
        /// will be &quot;Gilfoyle says It&apos;s not magic. It&apos;s talent and sweat&quot;.
        /// </param>
        /// <param name="source"> The source of the code logging the message. This could be the assembly name. </param>
        /// <param name="statusCode">
        /// If the message logged relates to a HTTP status code, you can put the code in this property. This would probably only be relevant for errors,
        /// 
        /// but could be used for logging successful status codes as well.
        /// </param>
        /// <param name="dateTime"> The date and time in UTC of the message. If you don&apos;t provide us with a value in dateTime, we will set the current date and time in UTC. </param>
        /// <param name="type"> The type of message. If logging an error, the type of the exception would go into type but you can put anything in there, that makes sense for your domain. </param>
        /// <param name="user"> An identification of the user triggering this message. You can put the users email address or your user key into this property. </param>
        /// <param name="severity"> An enum value representing the severity of this message. The following values are allowed: Verbose, Debug, Information, Warning, Error, Fatal. </param>
        /// <param name="url"> If message relates to a HTTP request, you may send the URL of that request. If you don&apos;t provide us with an URL, we will try to find a key named URL in serverVariables. </param>
        /// <param name="method"> If message relates to a HTTP request, you may send the HTTP method of that request. If you don&apos;t provide us with a method, we will try to find a key named REQUEST_METHOD in serverVariables. </param>
        /// <param name="version">
        /// Versions can be used to distinguish messages from different versions of your software. The value of version can be a SemVer compliant string or any other
        /// 
        /// syntax that you are using as your version numbering scheme.
        /// </param>
        /// <param name="correlationId">
        /// CorrelationId can be used to group similar log messages together into a single discoverable batch. A correlation ID could be a session ID from ASP.NET Core,
        /// 
        /// a unique string spanning multiple microsservices handling the same request, or similar.
        /// </param>
        /// <param name="cookies"> A key/value pair of cookies. This property only makes sense for logging messages related to web requests. </param>
        /// <param name="form"> A key/value pair of form fields and their values. This property makes sense if logging message related to users inputting data in a form. </param>
        /// <param name="queryString"> A key/value pair of query string parameters. This property makes sense if logging message related to a HTTP request. </param>
        /// <param name="serverVariables"> A key/value pair of server values. Server variables are typically related to handling requests in a webserver but could be used for other types of information as well. </param>
        /// <param name="data">
        /// A key/value pair of user-defined fields and their values. When logging an exception, the Data dictionary of
        /// 
        /// the exception is copied to this property. You can add additional key/value pairs, by modifying the Data
        /// 
        /// dictionary on the exception or by supplying additional key/values to this API.
        /// </param>
        internal Message(string id, string application, string detail, string hostname, string title, string titleTemplate, string source, int? statusCode, DateTimeOffset? dateTime, string type, string user, string severity, string url, string method, string version, string correlationId, IReadOnlyList<Item> cookies, IReadOnlyList<Item> form, IReadOnlyList<Item> queryString, IReadOnlyList<Item> serverVariables, IReadOnlyList<Item> data)
        {
            Id = id;
            Application = application;
            Detail = detail;
            Hostname = hostname;
            Title = title;
            TitleTemplate = titleTemplate;
            Source = source;
            StatusCode = statusCode;
            DateTime = dateTime;
            Type = type;
            User = user;
            Severity = severity;
            Url = url;
            Method = method;
            Version = version;
            CorrelationId = correlationId;
            Cookies = cookies;
            Form = form;
            QueryString = queryString;
            ServerVariables = serverVariables;
            Data = data;
        }

        /// <summary> The ID of this message. </summary>
        public string Id { get; }
        /// <summary> Used to identify which application logged this message. You can use this if you have multiple applications and services logging to the same log. </summary>
        public string Application { get; }
        /// <summary> A longer description of the message. For errors this could be a stacktrace, but it&apos;s really up to you what to log in there. </summary>
        public string Detail { get; }
        /// <summary> The hostname of the server logging the message. </summary>
        public string Hostname { get; }
        /// <summary> The textual title or headline of the message to log. </summary>
        public string Title { get; }
        /// <summary>
        /// The title template of the message to log. This property can be used from logging frameworks that supports
        /// 
        /// structured logging like: &quot;{user} says {quote}&quot;. In the example, titleTemplate will be this string and title
        /// 
        /// will be &quot;Gilfoyle says It&apos;s not magic. It&apos;s talent and sweat&quot;.
        /// </summary>
        public string TitleTemplate { get; }
        /// <summary> The source of the code logging the message. This could be the assembly name. </summary>
        public string Source { get; }
        /// <summary>
        /// If the message logged relates to a HTTP status code, you can put the code in this property. This would probably only be relevant for errors,
        /// 
        /// but could be used for logging successful status codes as well.
        /// </summary>
        public int? StatusCode { get; }
        /// <summary> The date and time in UTC of the message. If you don&apos;t provide us with a value in dateTime, we will set the current date and time in UTC. </summary>
        public DateTimeOffset? DateTime { get; }
        /// <summary> The type of message. If logging an error, the type of the exception would go into type but you can put anything in there, that makes sense for your domain. </summary>
        public string Type { get; }
        /// <summary> An identification of the user triggering this message. You can put the users email address or your user key into this property. </summary>
        public string User { get; }
        /// <summary> An enum value representing the severity of this message. The following values are allowed: Verbose, Debug, Information, Warning, Error, Fatal. </summary>
        public string Severity { get; }
        /// <summary> If message relates to a HTTP request, you may send the URL of that request. If you don&apos;t provide us with an URL, we will try to find a key named URL in serverVariables. </summary>
        public string Url { get; }
        /// <summary> If message relates to a HTTP request, you may send the HTTP method of that request. If you don&apos;t provide us with a method, we will try to find a key named REQUEST_METHOD in serverVariables. </summary>
        public string Method { get; }
        /// <summary>
        /// Versions can be used to distinguish messages from different versions of your software. The value of version can be a SemVer compliant string or any other
        /// 
        /// syntax that you are using as your version numbering scheme.
        /// </summary>
        public string Version { get; }
        /// <summary>
        /// CorrelationId can be used to group similar log messages together into a single discoverable batch. A correlation ID could be a session ID from ASP.NET Core,
        /// 
        /// a unique string spanning multiple microsservices handling the same request, or similar.
        /// </summary>
        public string CorrelationId { get; }
        /// <summary> A key/value pair of cookies. This property only makes sense for logging messages related to web requests. </summary>
        public IReadOnlyList<Item> Cookies { get; }
        /// <summary> A key/value pair of form fields and their values. This property makes sense if logging message related to users inputting data in a form. </summary>
        public IReadOnlyList<Item> Form { get; }
        /// <summary> A key/value pair of query string parameters. This property makes sense if logging message related to a HTTP request. </summary>
        public IReadOnlyList<Item> QueryString { get; }
        /// <summary> A key/value pair of server values. Server variables are typically related to handling requests in a webserver but could be used for other types of information as well. </summary>
        public IReadOnlyList<Item> ServerVariables { get; }
        /// <summary>
        /// A key/value pair of user-defined fields and their values. When logging an exception, the Data dictionary of
        /// 
        /// the exception is copied to this property. You can add additional key/value pairs, by modifying the Data
        /// 
        /// dictionary on the exception or by supplying additional key/values to this API.
        /// </summary>
        public IReadOnlyList<Item> Data { get; }
    }
}
