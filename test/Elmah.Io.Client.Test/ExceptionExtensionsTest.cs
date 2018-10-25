using NUnit.Framework;
using System;
using System.Linq;

namespace Elmah.Io.Client.Test
{
    public class ExceptionExtensionsTest
    {
        [Test]
        public void CanGenerateDataList()
        {
            // Arrange
            var innerException = new Exception("inner");
            innerException.Data.Add("inner-key", "inner-value");
            var outerException = new Exception("outer", innerException);
            outerException.Data.Add("outer-key", "outer-value");

            // Act
            var result = outerException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(r => r.Key == "inner-key" && r.Value == "inner-value"));
            Assert.That(result.Any(r => r.Key == "outer-key" && r.Value == "outer-value"));
        }

        [Test]
        public void CanHandleNoData()
        {
            // Arrange
            var exception = new Exception();

            // Act
            var result = exception.ToDataList();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanHandleNullException()
        {
            // Arrange
            Exception exception = null;

            // Act
            var result = exception.ToDataList();

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
