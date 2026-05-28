using NUnit.Framework;
using System.Collections.Generic;

namespace Elmah.Io.Client.Test
{
    public class AssemblyLoggerExtensionsTest
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("NonExistent.Assembly.That.Does.Not.Exist")]
        public void DoesNothingWhenAssemblyNameIsInvalidOrNotLoaded(string assemblyName)
        {
            var assemblies = new List<AssemblyInfo>();
            assemblies.TryAddAssemblyIfLoaded(assemblyName);
            Assert.That(assemblies, Is.Empty);
        }

        [Test]
        public void AddsAssemblyWhenLoaded()
        {
            var assemblies = new List<AssemblyInfo>();
            assemblies.TryAddAssemblyIfLoaded("Elmah.Io.Client");
            Assert.That(assemblies, Has.Count.EqualTo(1));
            Assert.That(assemblies[0].Name, Is.EqualTo("Elmah.Io.Client"));
            Assert.That(assemblies[0].Version, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void TryAddExtensionAssembliesIfLoadedDoesNotThrow()
        {
            var assemblies = new List<AssemblyInfo>();
            Assert.DoesNotThrow(() => assemblies.TryAddExtensionAssembliesIfLoaded());
        }

        [Test]
        public void TryAddExtensionAssembliesIfLoadedAddsNothingWhenExtensionsNotPresent()
        {
            var assemblies = new List<AssemblyInfo>();
            assemblies.TryAddExtensionAssembliesIfLoaded();
            // Extension assemblies are not loaded in the test project, so the list should be empty
            Assert.That(assemblies.Find(a => a.Name == "Elmah.Io.Client.Extensions.SourceCode"), Is.Null);
            Assert.That(assemblies.Find(a => a.Name == "Elmah.Io.Client.Extensions.Correlation"), Is.Null);
        }
    }
}
