using System;

namespace Elmah.Io.Client
{
    public partial class MessagesClient
    {
        public Action<CreateMessage> OnMessage { get; set; }
        public Action<CreateMessage, Exception> OnMessageFail { get; set; }
    }
}