using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Event args provided as part of the OnMessageFail event.
    /// </summary>
    /// <remarks>
    /// Create a new instance.
    /// </remarks>
    public class FailEventArgs(CreateMessage message, Exception error) : EventArgs
    {

        /// <summary>
        /// The message sent as part of the failing API request.
        /// </summary>
        public CreateMessage Message { get; set; } = message;

        /// <summary>
        /// The error happened as part of the failing API request.
        /// </summary>
        public Exception Error { get; set; } = error;
    }
}
