﻿using System;

namespace Elmah.Io.Client
{
    public partial class MessagesClient
    {
        /// <summary>
        /// By subscribing to the OnMessage event, you can hook into the pipeline of logging a message to elmah.io.
        /// The event is triggered just before calling elmah.io's API.
        /// </summary>
        public event EventHandler<MessageEventArgs> OnMessage;
        /// <summary>
        /// By subscribing to the OnMessageFail event, you can get a call if an error happened while logging a message
        /// to elmah.io. In this case you should do something to log the message elsewhere.
        /// </summary>
        public event EventHandler<FailEventArgs> OnMessageFail;
    }
}