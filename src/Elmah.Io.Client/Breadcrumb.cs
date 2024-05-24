// Don't use primary constructor here since that will cause properties not to be correctly set on partial class
#pragma warning disable IDE0290 // Use primary constructor
using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Create a new breadcrumb.
    /// </summary>
    public partial class Breadcrumb
    {
        /// <summary>
        /// Create a new breadcrumb.
        /// </summary>
        public Breadcrumb(DateTimeOffset? dateTime = default, string severity = default, string action = default, string message = default)
        {
            DateTime = dateTime;
            Severity = severity;
            Action = action;
            Message = message;
        }
    }
}
#pragma warning restore IDE0290 // Use primary constructor
