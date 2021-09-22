using System;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Event args used to signal a log message being created on elmah.io through the OnMessage event.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Create a new instance of the MessageEventArgs class. This is typically only called from within the elmah.io client.
        /// </summary>
        /// <param name="message">The created log messages</param>
        public MessageEventArgs(CreateMessage message)
        {
            Message = message;
        }

        /// <summary>
        /// The created log message.
        /// </summary>
        public CreateMessage Message { get; set; }
    }
}
