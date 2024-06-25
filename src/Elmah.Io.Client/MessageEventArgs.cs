using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Event args used to signal a log message being created on elmah.io through the OnMessage event.
    /// </summary>
    /// <remarks>
    /// Create a new instance of the MessageEventArgs class. This is typically only called from within the elmah.io client.
    /// </remarks>
    /// <param name="message">The created log messages</param>
    public class MessageEventArgs(CreateMessage message) : EventArgs
    {
        /// <summary>
        /// The created log message.
        /// </summary>
        public CreateMessage Message { get; set; } = message;
    }
}
