using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Event args provided as part of the OnMessageFail event.
    /// </summary>
    public class FailEventArgs : EventArgs
    {
        /// <summary>
        /// Create a new instance.
        /// </summary>
        public FailEventArgs(CreateMessage message, Exception error)
        {
            Message = message;
            Error = error;
        }

        /// <summary>
        /// The message sent as part of the failing API request.
        /// </summary>
        public CreateMessage Message { get; set; }

        /// <summary>
        /// The error happened as part of the failing API request.
        /// </summary>
        public Exception Error { get; set; }
    }
}
