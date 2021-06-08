using System;

namespace Elmah.Io.Client
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(CreateMessage message)
        {
            Message = message;
        }

        public CreateMessage Message { get; set; }
    }
}
