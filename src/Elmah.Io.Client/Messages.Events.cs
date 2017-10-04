using System;

namespace Elmah.Io.Client
{
    public partial class Messages
    {
        public event EventHandler<MessageEventArgs> OnMessage;
        public event EventHandler<FailEventArgs> OnMessageFail;
    }
}
