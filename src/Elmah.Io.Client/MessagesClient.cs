﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    /// <inheritdoc/>
    partial class MessagesClient : IMessagesClient
    {
        /// <summary>
        /// Create a new instances of the MessagesClient class using the provider options.
        /// This should typically not be called by any client. Use the ElmahioAPI.Create method instead.
        /// </summary>
        public MessagesClient(string baseUrl, HttpClient httpClient, ElmahIoOptions options) : this(baseUrl, httpClient)
        {
            Options = options;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance rules", "CA1822", Justification = "Method is not static in auto-generated class with this partial method")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "This is needed")]
        static partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.Formatting = Formatting.Indented;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            settings.Converters = [];
        }

        /// <summary>
        /// The options used to log messages to elmah.io.
        /// This should typically not be called by any client. Use the ElmahioAPI.Create method instead.
        /// </summary>
        public ElmahIoOptions Options { get; private set; }

        /// <inheritdoc/>
        public event EventHandler<MessageEventArgs> OnMessage;

        /// <inheritdoc/>
        public event EventHandler<FailEventArgs> OnMessageFail;

        /// <inheritdoc/>
        public event EventHandler<MessageFilterEventArgs> OnMessageFilter;

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
                Severity = severity.AsString(),
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "Keep returning null for backward compatibility")]
        public ICollection<CreateBulkMessageResult> CreateBulkAndNotify(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                if (ShouldFilter(message)) continue;
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            if (obfuscated.Count == 0) return null;

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
        public async Task<ICollection<CreateBulkMessageResult>> CreateBulkAndNotifyAsync(Guid logId, IList<CreateMessage> messages, CancellationToken cancellationToken = default)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                if (ShouldFilter(message)) continue;
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            if (obfuscated.Count == 0) return [];

            messages = obfuscated;

            try
            {
                return await CreateBulkAsync(logId.ToString(), messages, cancellationToken).ConfigureAwait(false);
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
            if (ShouldFilter(message)) return null;
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
        public async Task<Message> CreateAndNotifyAsync(Guid logId, CreateMessage message, CancellationToken cancellationToken = default)
        {
            if (ShouldFilter(message)) return null;
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            try
            {
                var messageResult = await CreateAsync(logId.ToString(), message, cancellationToken).ConfigureAwait(false);
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
            if (message.Form == null || message.Form.Count == 0) return message;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code style rules", "IDE0057", Justification = "Range doesn't work with old netstandard")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "This is needed")]
        private static Message MessageCreated(CreateMessageResult messageResult, CreateMessage message)
        {
            var location = messageResult.Location;
            var id = location?.AbsolutePath.Substring(1 + location.AbsolutePath.LastIndexOf('/'));

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

        private bool ShouldFilter(CreateMessage message)
        {
            if (OnMessageFilter == null) return false;

            var eventArgs = new MessageFilterEventArgs(message);
            OnMessageFilter(this, eventArgs);
            return eventArgs.Filter;
        }
    }
}
