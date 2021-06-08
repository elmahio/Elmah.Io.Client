using System;

namespace Elmah.Io.Client
{
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
