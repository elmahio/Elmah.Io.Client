using System;

namespace Elmah.Io.Client
{
    public partial class MessagesClient : IMessagesClient
    {
        /// <inheritdoc/>
        public event EventHandler<MessageEventArgs> OnMessage;
        
        /// <inheritdoc/>
        public event EventHandler<FailEventArgs> OnMessageFail;
    }
}