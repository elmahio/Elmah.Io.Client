using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Create a new breadcrumb.
    /// </summary>
    public partial class Breadcrumb(DateTimeOffset? dateTime = default, string severity = default, string action = default, string message = default)
    {
    }
}
