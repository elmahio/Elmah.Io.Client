using System;
using Elmah.Io.Client.Models;

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