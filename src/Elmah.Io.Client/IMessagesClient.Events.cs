using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial interface IMessagesClient
    {
        /// <summary>
        /// By subscribing to the OnMessage event, you can hook into the pipeline of logging a message to elmah.io.
        /// The event is triggered just before calling elmah.io's API.
        /// </summary>
        Action<CreateMessage> OnMessage { get; set; }

        /// <summary>
        /// By subscribing to the OnMessageFail event, you can get a call if an error happened while logging a message
        /// to elmah.io. In this case you should do something to log the message elsewhere.
        /// </summary>
        Action<CreateMessage, Exception> OnMessageFail { get; set; }
    }
}
