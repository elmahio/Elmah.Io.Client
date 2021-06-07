using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    public partial interface IMessagesClient
    {
        /// <summary>
        /// Write a log message with the Verbose severity.
        /// </summary>
        void Verbose(Guid logId, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Verbose severity and associated exception.
        /// </summary>
        void Verbose(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Debug severity.
        /// </summary>
        void Debug(Guid logId, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Debug severity and associated exception.
        /// </summary>
        void Debug(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Information severity.
        /// </summary>
        void Information(Guid logId, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Information severity and associated exception.
        /// </summary>
        void Information(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Warning severity.
        /// </summary>
        void Warning(Guid logId, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Warning severity and associated exception.
        /// </summary>
        void Warning(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Error severity.
        /// </summary>
        void Error(Guid logId, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Error severity and associated exception.
        /// </summary>
        void Error(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Fatal severity.
        /// </summary>
        void Fatal(Guid logId, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the Verbose Fatal and associated exception.
        /// </summary>
        void Fatal(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log message with the specified exception, severity and message.
        /// </summary>
        void Log(Guid logId, Exception exception, Severity severity, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateAndNotify method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        Message CreateAndNotify(Guid logId, CreateMessage message);

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateAndNotifyAsync method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        Task<Message> CreateAndNotifyAsync(Guid logId, CreateMessage message);

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateBulkAndNotify method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        ICollection<CreateBulkMessageResult> CreateBulkAndNotify(Guid logId, IList<CreateMessage> messages);

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateBulkAndNotifyAsync method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        Task<ICollection<CreateBulkMessageResult>> CreateBulkAndNotifyAsync(Guid logId, IList<CreateMessage> messages);
    }
}
