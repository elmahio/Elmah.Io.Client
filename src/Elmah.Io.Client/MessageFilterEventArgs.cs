using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Event args used to filter log messages from being sent to elmah.io through the OnMessageFilter event.
    /// </summary>
    /// <param name="message">The log message</param>
    public class MessageFilterEventArgs(CreateMessage message) : EventArgs
    {
        /// <summary>
        /// The log message.
        /// </summary>
        public CreateMessage Message { get; set; } = message;

        /// <summary>
        /// If set to true (default: false) the log messages will be filtered/ignored.
        /// </summary>
        public bool Filter { get; set; }
    }
}
