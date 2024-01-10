using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Extensions methods for the Severity enum.
    /// </summary>
    public static class SeverityExtensions
    {
        /// <summary>
        /// Returns the string representation of a Severity enum value.
        /// </summary>
        public static string AsString(this Severity severity)
        {
            return severity switch
            {
                Severity.Verbose => "Verbose",
                Severity.Debug => "Debug",
                Severity.Information => "Information",
                Severity.Warning => "Warning",
                Severity.Error => "Error",
                Severity.Fatal => "Fatal",
                _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null),
            };
        }
    }
}
