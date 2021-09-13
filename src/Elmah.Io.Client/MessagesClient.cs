using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    partial class MessagesClient : IMessagesClient
    {
        public MessagesClient(string baseUrl, HttpClient httpClient, ElmahIoOptions options) : this(baseUrl, httpClient)
        {
            Options = options;
        }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.Converters = new List<JsonConverter>();
        }

        public ElmahIoOptions Options { get; set; }

        /// <inheritdoc/>
        public event EventHandler<MessageEventArgs> OnMessage;

        /// <inheritdoc/>
        public event EventHandler<FailEventArgs> OnMessageFail;

        /// <inheritdoc/>
        public void Verbose(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Verbose(logId, null, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Verbose(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Verbose, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Debug(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Debug(logId, null, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Debug(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Debug, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Information(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Information(logId, null, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Information(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Information, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Warning(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Warning(logId, null, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Warning(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Warning, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Error(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Error(logId, null, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Error(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Error, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Fatal(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Fatal(logId, null, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
        public void Fatal(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Fatal, messageTemplate, propertyValues);
        }

        /// <inheritdoc/>
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
                message.Type = exception.GetBaseException().GetType().FullName;
            }

            CreateAndNotify(logId, message);
        }

        /// <inheritdoc/>
        public ICollection<CreateBulkMessageResult> CreateBulkAndNotify(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            messages = obfuscated;

            try
            {
                return CreateBulk(logId.ToString(), messages);
            }
            catch (Exception e)
            {
                foreach (var msg in messages)
                {
                    OnMessageFail?.Invoke(this, new FailEventArgs(msg, e));
                }
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<ICollection<CreateBulkMessageResult>> CreateBulkAndNotifyAsync(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            messages = obfuscated;

            try
            {
                return await CreateBulkAsync(logId.ToString(), messages).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                foreach (var msg in messages)
                {
                    OnMessageFail?.Invoke(this, new FailEventArgs(msg, e));
                }
                return null;
            }
        }

        /// <inheritdoc/>
        public Message CreateAndNotify(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            try
            {
                var messageResult = Create(logId.ToString(), message);
                return MessageCreated(messageResult, message);
            }
            catch (Exception e)
            {
                OnMessageFail?.Invoke(this, new FailEventArgs(message, e));
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<Message> CreateAndNotifyAsync(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            try
            {
                var messageResult = await CreateAsync(logId.ToString(), message).ConfigureAwait(false);
                return MessageCreated(messageResult, message);
            }
            catch (Exception e)
            {
                OnMessageFail?.Invoke(this, new FailEventArgs(message, e));
                return null;
            }
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
