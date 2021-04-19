using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;

namespace Elmah.Io.Client.Test
{
    public class IntegrationTest
    {
        // [Ignore("Only run manually")]
        [Test]
        public void Test()
        {
            var api = ElmahioAPI.Create("3376fd7e4f9a47cba704dc3e37c14d2a"); // API key must have all permissions enabled

            var now = DateTime.UtcNow.Ticks.ToString();

            // Get all logs
            var logs = api.Logs.GetAll();

            // Create log
            api.Logs.Create(new CreateLog
            {
                Name = now
            });

            Thread.Sleep(2000);

            var logs2 = api.Logs.GetAll();
            Assert.That(logs2.Count == 1 + logs.Count);

            var created = logs2.FirstOrDefault(l => l.Name == now);
            Assert.That(created, Is.Not.Null);

            // Get single log
            var log = api.Logs.Get(created.Id);
            Assert.That(log, Is.Not.Null);

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

            // Create message

            Assert.Throws<ElmahIoClientException<CreateMessageResult>>(() =>
                api.Messages.Create(log.Id, new CreateMessage
                {
                    Title = now
                })
            );

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

            // Delete messages

            api.Messages.Delete(message.Id, log.Id);

            Thread.Sleep(2000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(0));

            // Create bulk

            api
                .Messages
                .CreateBulk(log.Id, new[]
                {
                    new CreateMessage {Title = now},
                    new CreateMessage {Title = now},
                }
                .ToList());

            Thread.Sleep(5000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(2));

            // Delete bulk

            api.Messages.DeleteAll(log.Id, new Search { Query = "*:*" });

            Thread.Sleep(5000);

            Assert.That(api.Messages.GetAll(log.Id).Total, Is.EqualTo(0));
        }
    }
}
