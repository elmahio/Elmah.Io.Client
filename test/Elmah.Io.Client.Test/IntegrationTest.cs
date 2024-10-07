using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Elmah.Io.Client.Test
{
    public class IntegrationTest
    {
        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2925:\"Thread.Sleep\" should not be used in tests", Justification = "Making sure that data is saved")]
        public void Test()
        {
            var baseUrl = Environment.GetEnvironmentVariable("SECRET_BASE_URL");
            var apiKey = Environment.GetEnvironmentVariable("SECRET_API_KEY");
            var logId = Environment.GetEnvironmentVariable("SECRET_LOG_ID");
            var heartbeatId = Environment.GetEnvironmentVariable("SECRET_HEARTBEAT_ID");

            // Only run the test if the environment variables are specified
            if (string.IsNullOrWhiteSpace(baseUrl)
                || string.IsNullOrWhiteSpace(apiKey)
                || string.IsNullOrWhiteSpace(logId)
                || string.IsNullOrWhiteSpace(heartbeatId))
                Assert.Ignore("Missing environment variables");

            var frameworkVersion = "462";
#if NETSTANDARD1_1_OR_GREATER || NET6_0_OR_GREATER
            frameworkVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
#endif
            var domain = Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri url) ? url.Host : "unknown";

            var api = ElmahioAPI.Create(apiKey, new ElmahIoOptions(), new HttpClient { BaseAddress = new Uri(baseUrl) }); // API key must have all permissions enabled

            var now = DateTime.UtcNow.Ticks.ToString();

            #region Logs

            // Get all logs
            var logs = api.Logs.GetAll();

            // Create log
            var logName = $"{frameworkVersion}-{domain}-{now}";
            api.Logs.Create(new CreateLog
            {
                Name = logName,
            });

            Thread.Sleep(2000);

            var logs2 = api.Logs.GetAll();
            Assert.That(logs2.Count == 1 + logs.Count);

            var created = logs2.FirstOrDefault(l => l.Name == logName);
            Assert.That(created, Is.Not.Null);

            // Get single log
            var log = api.Logs.Get(created.Id);
            Assert.That(log, Is.Not.Null);

            // Disable log
            api.Logs.Disable(created.Id);

            Thread.Sleep(2000);
            log = api.Logs.Get(created.Id);
            Assert.That(log.Disabled, Is.True);

            // Enable log
            api.Logs.Enable(created.Id);

            Thread.Sleep(2000);
            log = api.Logs.Get(created.Id);
            Assert.That(log.Disabled, Is.False);

            // Diagnose
            var result = api.Logs.Diagnose(created.Id);
            Assert.That(result.Count, Is.EqualTo(0));

            #endregion

            #region SourceMaps

            // Create source map
            using var sourceMapStream = new MemoryStream(Encoding.UTF8.GetBytes("{ version: 3, file: \"path.min.js\", sourceRoot: \"\", sources: [\"path.js\"], names: [\"src\", mappings: \"\" }"));
            using var scriptStream = new MemoryStream(Encoding.UTF8.GetBytes("var v = 42;"));
            api.SourceMaps.CreateOrUpdate(
                log.Id,
                new Uri("/some/path.min.js", UriKind.Relative),
                new FileParameter(sourceMapStream, "path.map", "application/json"),
                new FileParameter(scriptStream, "path.min.js", "text/javascript"));

            #endregion

            #region Deployments

            // Create deployment
            api.Deployments.Create(new CreateDeployment
            {
                Version = now
            });

            Thread.Sleep(2000);

            var deployments2 = api.Deployments.GetAll();
            Assert.That(deployments2.First().Version == now);

            var createdDeployment = deployments2.FirstOrDefault(d => d.Version == now);
            Assert.That(createdDeployment, Is.Not.Null);

            // Get single deployment
            var deployment = api.Deployments.Get(createdDeployment.Id);
            Assert.That(deployment, Is.Not.Null);

            // Delete deployment
            api.Deployments.Delete(deployment.Id);

            Thread.Sleep(2000);

            var deployments = api.Deployments.GetAll();
            Assert.That(deployments.All(d => d.Version != now));

            #endregion

            #region Messages

            // Create message

            api.Messages.Create(log.Id, new CreateMessage
            {
                Title = now
            });

            Thread.Sleep(2000);

            // Get all messages
            var messages = api.Messages.GetAll(log.Id);
            Assert.That(messages, Is.Not.Null);
            Assert.That(messages.Total, Is.EqualTo(1));
            Assert.That(messages.Messages.Count, Is.EqualTo(1));

            var message = messages.Messages.First();

            // Get single message

            var createdMessage = api.Messages.Get(message.Id, log.Id);
            Assert.That(createdMessage, Is.Not.Null);

            // Hide message

            api.Messages.Hide(message.Id, log.Id);

            Thread.Sleep(2000);

            // Fix message

            api.Messages.Fix(message.Id, log.Id);

            Thread.Sleep(2000);

            // Fix messages

            api.Messages.FixAll(log.Id, new Search
            {
                Query = "*:*"
            });

            Thread.Sleep(2000);

            // Delete messages

            api.Messages.Delete(message.Id, log.Id);

            Thread.Sleep(2000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(0));

            // Create bulk

            api
                .Messages
                .CreateBulk(log.Id,
                [
                    new CreateMessage {Title = now},
                    new CreateMessage {Title = now},
                ]);

            Thread.Sleep(5000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(2));

            // Delete bulk

            api.Messages.DeleteAll(log.Id, new Search { Query = "*:*" });

            Thread.Sleep(5000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(0));

            // Filter

            api.Messages.OnMessageFilter += (sender, args) =>
            {
                args.Filter = true;
            };

            api.Messages.CreateAndNotify(new Guid(log.Id), new CreateMessage
            {
                Title = now
            });

            Thread.Sleep(2000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(0));

            #endregion

            #region Heartbeats

            api.Heartbeats.Create(heartbeatId, logId, new CreateHeartbeat
            {
                Result = "Unhealthy",
                Reason = "To have something to look at",
                Application = "My app",
                Checks = [
                    new() {
                        Result = "Healthy",
                        Name = "check1"
                    },
                    new() {
                        Result = "Unhealthy",
                        Name = "check2"
                    }
                ]
            });

            #endregion

            // Delete
            api.Logs.Delete(log.Id);
            Thread.Sleep(2000);
            Assert.That(api.Logs.GetAll().Count == logs.Count);
        }
    }
}
