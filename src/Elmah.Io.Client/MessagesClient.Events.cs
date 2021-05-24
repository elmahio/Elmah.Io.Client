using System;

namespace Elmah.Io.Client
{
    public partial class MessagesClient
    {
        public Action<CreateMessage> OnMessage;
        public Action<CreateMessage, Exception> OnMessageFail;
    }
}