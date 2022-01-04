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
            switch (severity)
            {
                case Severity.Verbose:
                    return "Verbose";
                case Severity.Debug:
                    return "Debug";
                case Severity.Information:
                    return "Information";
                case Severity.Warning:
                    return "Warning";
                case Severity.Error:
                    return "Error";
                case Severity.Fatal:
                    return "Fatal";
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
        }
    }
}
