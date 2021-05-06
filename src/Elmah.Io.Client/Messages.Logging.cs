using System;
using Elmah.Io.Client.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Rest;

namespace Elmah.Io.Client
{
    public partial class Messages
    {
        public void Verbose(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Verbose(logId, null, messageTemplate, propertyValues);
        }

        public void Verbose(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Verbose, messageTemplate, propertyValues);
        }

        public void Debug(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Debug(logId, null, messageTemplate, propertyValues);
        }

        public void Debug(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Debug, messageTemplate, propertyValues);
        }

        public void Information(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Information(logId, null, messageTemplate, propertyValues);
        }

        public void Information(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Information, messageTemplate, propertyValues);
        }

        public void Warning(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Warning(logId, null, messageTemplate, propertyValues);
        }

        public void Warning(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Warning, messageTemplate, propertyValues);
        }

        public void Error(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Error(logId, null, messageTemplate, propertyValues);
        }

        public void Error(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Error, messageTemplate, propertyValues);
        }

        public void Fatal(Guid logId, string messageTemplate, params object[] propertyValues)
        {
            Fatal(logId, null, messageTemplate, propertyValues);
        }

        public void Fatal(Guid logId, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log(logId, exception, Severity.Fatal, messageTemplate, propertyValues);
        }

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

        public IList<CreateBulkMessageResult> CreateBulkAndNotify(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            messages = obfuscated;

            return Task.Factory.StartNew(s =>
            {
                return
                   CreateBulkWithHttpMessagesAsync(logId.ToString(), messages)
                   .ContinueWith(BulkMessagesCreated(messages));
            }, this, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }

        public async Task<IList<CreateBulkMessageResult>> CreateBulkAndNotifyAsync(Guid logId, IList<CreateMessage> messages)
        {
            var obfuscated = new List<CreateMessage>();
            foreach (var message in messages)
            {
                OnMessage?.Invoke(this, new MessageEventArgs(message));
                obfuscated.Add(Obfuscate(message));
            }

            messages = obfuscated;

            return await CreateBulkWithHttpMessagesAsync(logId.ToString(), messages)
                .ContinueWith(BulkMessagesCreated(messages))
                .ConfigureAwait(false);
        }

        public Message CreateAndNotify(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            return Task.Factory.StartNew(s =>
            {
                return
                   CreateWithHttpMessagesAsync(logId.ToString(), message)
                   .ContinueWith(MessagesCreated(message));
            }, this, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }

        public async Task<Message> CreateAndNotifyAsync(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
            message = Obfuscate(message);
            return await
               CreateWithHttpMessagesAsync(logId.ToString(), message)
               .ContinueWith(MessagesCreated(message))
               .ConfigureAwait(false);
        }

        private CreateMessage Obfuscate(CreateMessage message)
        {
            if (message == null) return message;
            if (message.Form == null || !message.Form.Any()) return message;
            if (Client?.Options?.FormKeysToObfuscate == null) return message;

            foreach (var key in Client.Options.FormKeysToObfuscate.Select(x => x.ToLower()))
            {
                foreach (var f in message.Form.Where(f => f.Key.ToLower().Equals(key) && !string.IsNullOrWhiteSpace(f.Value)))
                {
                    f.Value = new string('*', f.Value.Length);
                }
            }

            return message;
        }

        private Func<Task<Microsoft.Rest.HttpOperationResponse<CreateMessageResult>>, Message> MessagesCreated(CreateMessage message)
        {
            return a =>
            {
                if (a.Status != TaskStatus.RanToCompletion)
                {
                    OnMessageFail?.Invoke(this, new FailEventArgs(message, a.Exception));
                    return null;
                }

                var location = a.Result?.Response?.Headers?.Location;
                var id = location?.AbsolutePath.Substring(1 + location.AbsolutePath.LastIndexOf("/", StringComparison.Ordinal));

                return new Message(id, message.Application, message.Detail, message.Hostname, message.Title,
                    message.TitleTemplate, message.Source, message.StatusCode, message.DateTime, message.Type,
                    message.User, message.Severity, message.Url, message.Method, message.Version, message.CorrelationId,
                    message.Cookies, message.Form, message.QueryString, message.ServerVariables, message.Data,
                    message.Breadcrumbs);
            };
        }

        private Func<Task<HttpOperationResponse<IList<CreateBulkMessageResult>>>, IList<CreateBulkMessageResult>> BulkMessagesCreated(IList<CreateMessage> messages)
        {
            return a =>
            {
                if (a.Status != TaskStatus.RanToCompletion)
                {
                    foreach (var msg in messages)
                    {
                        OnMessageFail?.Invoke(this, new FailEventArgs(msg, a.Exception));
                    }
                    return null;
                }

                return a.Result?.Body;
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