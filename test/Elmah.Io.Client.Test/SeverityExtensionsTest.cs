using NUnit.Framework;
using System;
using System.Linq;

namespace Elmah.Io.Client.Test
{
    public class SeverityExtensionsTest
    {
        [Test]
        public void CanResolveString()
        {
            foreach (var enumValue in Enum.GetValues(typeof(Severity)).Cast<Severity>())
            {
                Assert.That(enumValue.AsString(), Is.EqualTo(enumValue.ToString()));
            }
        }
    }
}
