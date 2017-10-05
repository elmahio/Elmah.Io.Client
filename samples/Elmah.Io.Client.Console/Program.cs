using System;
using System.Collections.Generic;
using Elmah.Io.Client.Models;

namespace Elmah.Io.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = ElmahioAPI.Create("API_KEY");
            var logId = new Guid("LOG_ID");

            // Examples of severity helper methods
            client.Messages.Fatal(logId, new ApplicationException("A fatal exception"), "Fatal message");
            client.Messages.Error(logId, new ApplicationException("An exception"), "Error message");
            client.Messages.Warning(logId, "A warning");
            client.Messages.Information(logId, "An info message");
            client.Messages.Debug(logId, "A debug message");
            client.Messages.Verbose(logId, "A verbose message");

            // Example of using OnMessage event to decorate all messages before sending to elmah.io
            client.Messages.OnMessage += (sender, eventArgs) =>
            {
                eventArgs.Message.Version = "1.0.0";
            };

            // Example of creating a log message with full control over all properties
            client.Messages.CreateAndNotify(logId, new CreateMessage
            {
                Title = "Hello World",
                Application = "Elmah.Io.Client sample",
                Detail = "This is a long description of the error. Maybe even a stacktrace",
                Severity = Severity.Error.ToString(),
                Data = new List<Item>
                {
                    new Item {Key = "Username", Value = "Man in black"}
                }
            });
        }
    }
}
