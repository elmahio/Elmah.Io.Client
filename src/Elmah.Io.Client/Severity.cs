namespace Elmah.Io.Client
{
    /// <summary>
    /// Helper enum to generate valid severities.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// A verbose log message.
        /// </summary>
        Verbose,
        /// <summary>
        /// A debug log message.
        /// </summary>
        Debug,
        /// <summary>
        /// An information log message.
        /// </summary>
        Information,
        /// <summary>
        /// A warning log message.
        /// </summary>
        Warning,
        /// <summary>
        /// An error log message.
        /// </summary>
        Error,
        /// <summary>
        /// A fatal log message.
        /// </summary>
        Fatal,
    }
}