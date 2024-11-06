using NUnit.Framework;

namespace Elmah.Io.Client.Test
{
    public class ApplicationInfoHelperTest
    {
        [Test]
        public void CanGetApplicationType() => Assert.That(ApplicationInfoHelper.GetApplicationType(), Is.EqualTo("console"));
    }
}
