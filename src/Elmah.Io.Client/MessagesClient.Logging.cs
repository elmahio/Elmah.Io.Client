using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace Elmah.Io.Client
{
    public partial class MessagesClient
    {
        /// <summary>
        /// Write a log message with the Verbose severity.
        /// </summary>
        public void Verbose(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Verbose(logId, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Verbose severity and associated exception.
        /// </summary>
        public void Verbose(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Verbose, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Debug severity.
        /// </summary>
        public void Debug(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Debug(logId, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Debug severity and associated exception.
        /// </summary>
        public void Debug(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Debug, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Information severity.
        /// </summary>
        public void Information(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Information(logId, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Information severity and associated exception.
        /// </summary>
        public void Information(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Information, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Warning severity.
        /// </summary>
        public void Warning(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Warning(logId, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Warning severity and associated exception.
        /// </summary>
        public void Warning(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Warning, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Error severity.
        /// </summary>
        public void Error(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Error(logId, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Error severity and associated exception.
        /// </summary>
        public void Error(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Error, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Fatal severity.
        /// </summary>
        public void Fatal(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Fatal(logId, null, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the Verbose Fatal and associated exception.
        /// </summary>
        public void Fatal(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Fatal, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log message with the specified exception, severity and message.
        /// </summary>
        public void Log(Guid logId, Exception exception, Severity severity, string messageTemplate, params object[] propertyValues)
        {
            var message = new CreateMessage
            {
                DateTime = DateTime.UtcNow,
                Title = string.Format(messageTemplate, propertyValues),
                Severity = SeverityToString(severity)
            };
            if (exception != null)
            {
                message.Detail = exception.ToString();
                message.Data = exception.ToDataList();
                message.Type = exception.GetType().Name;
            }

            CreateAndNotify(logId, message);
        }

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateBulkAndNotify method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        public ICollection<CreateBulkMessageResult> CreateBulkAndNotify(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            messages = obfuscated;

            return CreateBulk(logId.ToString(), messages);
        }

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateBulkAndNotifyAsync method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        public async Task<ICollection<CreateBulkMessageResult>> CreateBulkAndNotifyAsync(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            messages = obfuscated;

            return await CreateBulkAsync(logId.ToString(), messages).ConfigureAwait(false);
        }

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateAndNotify method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        public Message CreateAndNotify(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            var messageResult = Create(logId.ToString(), message);
            return MessageCreated(messageResult, message);
        }

        /// <summary>
        /// Low level log method, which all other methods wanting to log a log message should ideally call.
        /// The CreateAndNotifyAsync method triggers event handlers of the OnMessage and OnMessageFail events.
        /// </summary>
        public async Task<Message> CreateAndNotifyAsync(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            var messageResult = await CreateAsync(logId.ToString(), message).ConfigureAwait(false);
            return MessageCreated(messageResult, message);
        }

        private CreateMessage Obfuscate(CreateMessage message)
        {
            if (message == null) return message;
            if (message.Form == null || !message.Form.Any()) return message;
            if (Options?.FormKeysToObfuscate == null) return message;

            foreach (var key in Options.FormKeysToObfuscate.Select(x => x.ToLower()))
            {
                foreach (var f in message.Form.Where(f => f.Key.ToLower().Equals(key) && !string.IsNullOrWhiteSpace(f.Value)))
                {
                    f.Value = new string('*', f.Value.Length);
                }
            }

            return message;
        }

        private Message MessageCreated(CreateMessageResult messageResult, CreateMessage message)
        {
            var location = messageResult.Location;
            var id = location?.AbsolutePath.Substring(1 + location.AbsolutePath.LastIndexOf("/", StringComparison.Ordinal));

            return new Message()
            {
                Id = id,
                Application = message.Application,
                Detail = message.Detail,
                Hostname = message.Hostname,
                Title = message.Title,
                TitleTemplate = message.TitleTemplate,
                Source = message.Source,
                StatusCode = message.StatusCode,
                DateTime = message.DateTime,
                Type = message.Type,
                User = message.User,
                Severity = message.Severity,
                Url = message.Url,
                Method = message.Method,
                Version = message.Version,
                CorrelationId = message.CorrelationId,
                Cookies = message.Cookies,
                Form = message.Form,
                QueryString = message.QueryString,
                ServerVariables = message.ServerVariables,
                Data = message.Data,
                Breadcrumbs = message.Breadcrumbs,
            };
        }

        private string SeverityToString(Severity severity)
        {
            switch (severity)
            {
                case Severity.Verbose:
                    return "Verbose";
                case Severity.Debug:
                    return "Debug";
                case Severity.Information:
                    return "Information";
                case Severity.Warning:
                    return "Warning";
                case Severity.Error:
                    return "Error";
                case Severity.Fatal:
                    return "Fatal";
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
        }
    }
}