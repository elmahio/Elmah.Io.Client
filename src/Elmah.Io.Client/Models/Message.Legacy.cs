using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.Io.Client.Models
{
    public partial class Message
    {
        /// <summary>
        /// Initializes a new instance of the Message class.
        /// </summary>
        /// <param name="id">The ID of this message.</param>
        /// <param name="application">Used to identify which application logged
        /// this message. You can use this if you have multiple applications
        /// and services logging to the same log</param>
        /// <param name="detail">A longer description of the message. For
        /// errors this could be a stacktrace, but it's really up to you what
        /// to log in there.</param>
        /// <param name="hostname">The hostname of the server logging the
        /// message.</param>
        /// <param name="title">The textual title or headline of the message to
        /// log.</param>
        /// <param name="source">The source of the code logging the message.
        /// This could be the assembly name.</param>
        /// <param name="statusCode">If the message logged relates to a HTTP
        /// status code, you can put the code in this property. This would
        /// probably only be relevant for errors,
        /// but could be used for logging successful status codes as
        /// well.</param>
        /// <param name="dateTime">The date and time in UTC of the message. If
        /// you don't provide us with a value in dateTime, we will set the
        /// current date and time in UTC.</param>
        /// <param name="type">The type of message. If logging an error, the
        /// type of the exception would go into type but you can put anything
        /// in there, that makes sense for your domain.</param>
        /// <param name="user">An identification of the user triggering this
        /// message. You can put the users email address or your user key into
        /// this property.</param>
        /// <param name="severity">An enum value representing the severity of
        /// this message. The following values are allowed: Verbose, Debug,
        /// Information, Warning, Error, Fatal</param>
        /// <param name="url">If message relates to a HTTP request, you may
        /// send the URL of that request. If you don't provide us with an URL,
        /// we will try to find a key named URL in serverVariables.</param>
        /// <param name="method">If message relates to a HTTP request, you may
        /// send the HTTP method of that request. If you don't provide us with
        /// a method, we will try to find a key named REQUEST_METHOD in
        /// serverVariables.</param>
        /// <param name="version">Versions can be used to distinguish messages
        /// from different versions of your software. The value of version can
        /// be a SemVer compliant string or any other
        /// syntax that you are using as your version numbering scheme.</param>
        /// <param name="cookies">A key/value pair of cookies. This property
        /// only makes sense for logging messages related to web
        /// requests.</param>
        /// <param name="form">A key/value pair of form fields and their
        /// values. This property makes sense if logging message related to
        /// users inputting data in a form.</param>
        /// <param name="queryString">A key/value pair of query string
        /// parameters. This property makes sense if logging message related to
        /// a HTTP request.</param>
        /// <param name="serverVariables">A key/value pair of server values.
        /// Server variables are typically related to handling requests in a
        /// webserver but could be used for other types of information as
        /// well.</param>
        /// <param name="data">A key/value pair of user-defined fields and
        /// their values. When logging an exception, the Data dictionary of
        /// the exception is copied to this property. You can add additional
        /// key/value pairs, by modifying the Data
        /// dictionary on the exception or by supplying additional key/values
        /// to this API.</param>
        [Obsolete("Use the overload accepting a titleTemplate")]
        public Message(string id = default(string), string application = default(string), string detail = default(string), string hostname = default(string), string title = default(string), string source = default(string), int? statusCode = default(int?), System.DateTime? dateTime = default(System.DateTime?), string type = default(string), string user = default(string), string severity = default(string), string url = default(string), string method = default(string), string version = default(string), IList<Item> cookies = default(IList<Item>), IList<Item> form = default(IList<Item>), IList<Item> queryString = default(IList<Item>), IList<Item> serverVariables = default(IList<Item>), IList<Item> data = default(IList<Item>))
            : this(id, application, detail, hostname, title, null, source, statusCode, dateTime, type, user, severity, url, method, version, cookies, form, queryString, serverVariables, data)
        {
        }
    }
}
