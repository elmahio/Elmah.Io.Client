using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Elmah.Io.Client.Test
{
    public class ExceptionExtensionsTest
    {
        [Test]
        public void CanGenerateDataListWithHelpLink()
        {
            // Arrange
            var exception = new Exception();
            exception.HelpLink = "http://localhost";

            // Act
            var result = exception.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "Exception.HelpLink" && r.Value == "http://localhost"));
        }

        [Test]
        public void CanGenerateDataListFromArgumentException()
        {
            // Arrange
            var argumentException = new ArgumentException("message", "paramName");

            // Act
            var result = argumentException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "ArgumentException.ParamName" && r.Value == "paramName"));
        }

        [Test]
        public void CanGenerateDataListFromArgumentNullException()
        {
            // Arrange
            var argumentNullException = new ArgumentNullException("paramName");

            // Act
            var result = argumentNullException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "ArgumentNullException.ParamName" && r.Value == "paramName"));
        }

        [Test]
        public void CanGenerateDataListFromArgumentOutOfRangeException()
        {
            // Arrange
            var argumentOutOfRangeException = new ArgumentOutOfRangeException("paramName");

            // Act
            var result = argumentOutOfRangeException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "ArgumentOutOfRangeException.ParamName" && r.Value == "paramName"));
        }

        [Test]
        public void CanGenerateDataListFromSocketException()
        {
            // Arrange
            var socketException = new System.Net.Sockets.SocketException(100);

            // Act
            var result = socketException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(r => r.Key == "SocketException.SocketErrorCode" && r.Value == "100"));
            Assert.That(result.Any(r => r.Key == "SocketException.ErrorCode" && r.Value == "100"));
        }

        [Test]
        public void CanGenerateDataListFromBadImageFormatException()
        {
            // Arrange
            var badImageFormatException = new BadImageFormatException("message", "fileName");

            // Act
            var result = badImageFormatException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "BadImageFormatException.FileName" && r.Value == "fileName"));
        }

        [Test]
        public void CanGenerateDataListFromFileNotFoundException()
        {
            // Arrange
            var fileNotFoundException = new FileNotFoundException("message", "fileName");

            // Act
            var result = fileNotFoundException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "FileNotFoundException.FileName" && r.Value == "fileName"));
        }

        [Test]
        public void CanGenerateDataListFromWebException()
        {
            // Arrange
            var webException = new WebException("message", WebExceptionStatus.ProtocolError);

            // Act
            var result = webException.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Any(r => r.Key == "WebException.Status" && r.Value == "ProtocolError"));
        }

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
            Assert.That(result.Any(r => r.Key == "Exception.inner-key" && r.Value == "inner-value"));
            Assert.That(result.Any(r => r.Key == "Exception.outer-key" && r.Value == "outer-value"));
        }

        [Test]
        public void CanGenerateDataListFromAggregateException()
        {
            // Arrange
            var innerException1 = new Exception("inner1");
            innerException1.Data.Add("inner-key1", "inner-value1");
            var innerException2 = new Exception("inner2");
            innerException2.Data.Add("inner-key2", "inner-value2");
            var innerException3 = new Exception("inner3");
            innerException3.Data.Add("inner-key3", "inner-value3");

            var aggregateException1 = new AggregateException(innerException1, innerException2);
            var aggregateException2 = new AggregateException(innerException3);
            var aggregateException3 = new AggregateException(aggregateException1, aggregateException2);

            // Act
            var result = aggregateException3.ToDataList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Any(r => r.Key == "Exception.inner-key1" && r.Value == "inner-value1"));
            Assert.That(result.Any(r => r.Key == "Exception.inner-key2" && r.Value == "inner-value2"));
            Assert.That(result.Any(r => r.Key == "Exception.inner-key3" && r.Value == "inner-value3"));
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
