using System;
using Elmah.Io.Client.Models;

namespace Elmah.Io.Client
{
    public class FailEventArgs : EventArgs
    {
        public FailEventArgs(CreateMessage message, Exception error)
        {
            Message = message;
            Error = error;
        }

        public CreateMessage Message { get; set; }

        public Exception Error { get; set; }
    }
}