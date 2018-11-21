using Elmah.Io.Client.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.Client.Test
{
    public class CreateMessageExtensionsTest
    {
        [Test]
        public void CanObfuscate()
        {
            var message = new CreateMessage
            {
                Form = new List<Item>
                {
                    new Item { Key = "Random", Value = "Value" },
                    new Item { Key = "Password", Value = "someverysecretpassword" }
                }
            };

            var result = message.ObfuscatePasswords();

            Assert.That(result.Form.Single(f => f.Key == "Random").Value, Is.EqualTo("Value"));
            Assert.That(result.Form.Single(f => f.Key == "Password").Value, Is.EqualTo("**********************"));
        }
    }
}
