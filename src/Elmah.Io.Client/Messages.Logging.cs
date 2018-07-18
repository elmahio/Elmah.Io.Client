using System;
using Elmah.Io.Client.Models;
using System.Threading.Tasks;
using System.Threading;

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

        [Obsolete]
        public void CreateAndNotify(string logId, CreateMessage message)
        {
            CreateAndNotify(new Guid(logId), message);
        }

        public Message CreateAndNotify(Guid logId, CreateMessage message)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message));
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
            return await
               CreateWithHttpMessagesAsync(logId.ToString(), message)
               .ContinueWith(MessagesCreated(message))
               .ConfigureAwait(false);
        }

        private Func<Task<Microsoft.Rest.HttpOperationResponse>, Message> MessagesCreated(CreateMessage message)
        {
            return a =>
            {
                if (a.Status != TaskStatus.RanToCompletion)
                {
                    OnMessageFail?.Invoke(this, new FailEventArgs(message, a.Exception));
                }

                var location = a.Result?.Response?.Headers?.Location;
                var id = location?.AbsolutePath.Substring(1 + location.AbsolutePath.LastIndexOf("/", StringComparison.Ordinal));

                return new Message(id, message.Application, message.Detail, message.Hostname, message.Title,
                    message.Source, message.StatusCode, message.DateTime, message.Type, message.User,
                    message.Severity, message.Url, message.Method, message.Version, message.Cookies, message.Form,
                    message.QueryString, message.ServerVariables, message.Data);
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