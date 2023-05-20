using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Elmah.Io.Client.Test
{
    public class ExceptionExtensionsTest
    {
        [Test]
        public void CanGenerateCreateMessageFromException()
        {
            // Arrange
            var ex = new ApplicationException("An exception");
            ex.Data.Add("Hello", "World");

            // Act
            var msg = ex.ToMessage();

            // Assert
            Assert.That(msg, Is.Not.Null);
            Assert.That(msg.Title, Is.EqualTo(ex.Message));
            Assert.That(msg.Type, Is.EqualTo(ex.GetType().FullName));
            Assert.That(msg.DateTime.HasValue);
            Assert.That(msg.Severity, Is.EqualTo("Error"));
            Assert.That(msg.Detail, Is.EqualTo(ex.ToString()));
            Assert.That(msg.Data, Is.Not.Null);
            Assert.That(msg.Data.Any(d => d.Key == "ApplicationException.Hello" && d.Value == "World"));
        }

        [Test]
        public void CanGenerateDataListWithHelpLink()
        {
            // Arrange
            var exception = new Exception();
            exception.HelpLink = "http://localhost";

            // Act
            var result = exception.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "Exception.HelpLink" && d.Value == "http://localhost"));
        }

        [Test]
        public void CanGenerateDataListFromArgumentException()
        {
            // Arrange
            var argumentException = new ArgumentException("message", "paramName");

            // Act
            var result = argumentException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "ArgumentException.ParamName" && d.Value == "paramName"));
        }

        [Test]
        public void CanGenerateDataListFromArgumentNullException()
        {
            // Arrange
            var argumentNullException = new ArgumentNullException("paramName");

            // Act
            var result = argumentNullException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "ArgumentNullException.ParamName" && d.Value == "paramName"));
        }

        [Test]
        public void CanGenerateDataListFromArgumentOutOfRangeException()
        {
            // Arrange
            var argumentOutOfRangeException = new ArgumentOutOfRangeException("paramName");

            // Act
            var result = argumentOutOfRangeException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "ArgumentOutOfRangeException.ParamName" && d.Value == "paramName"));
        }

        [Test]
        public void CanGenerateDataListFromSocketException()
        {
            // Arrange
            var socketException = new System.Net.Sockets.SocketException(100);

            // Act
            var result = socketException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "SocketException.SocketErrorCode" && d.Value == "100"));
            Assert.That(result.Any(d => d.Key == "SocketException.ErrorCode" && d.Value == "100"));
        }

        [Test]
        public void CanGenerateDataListFromBadImageFormatException()
        {
            // Arrange
            var badImageFormatException = new BadImageFormatException("message", "fileName");

            // Act
            var result = badImageFormatException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "BadImageFormatException.FileName" && d.Value == "fileName"));
        }

        [Test]
        public void CanGenerateDataListFromFileNotFoundException()
        {
            // Arrange
            var fileNotFoundException = new FileNotFoundException("message", "fileName");

            // Act
            var result = fileNotFoundException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "FileNotFoundException.FileName" && d.Value == "fileName"));
        }

        [Test]
        public void CanGenerateDataListFromWebException()
        {
            // Arrange
            var webException = new WebException("message", WebExceptionStatus.ProtocolError);

            // Act
            var result = webException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "WebException.Status" && d.Value == "ProtocolError"));
        }

        [Test]
        public void CanGenerateDataListFromTaskCanceledExceptionNotCanceled()
        {
            // Arrange
            var task = Task.FromResult(0);
            var taskCanceledException = new TaskCanceledException(task);

            // Act
            var result = taskCanceledException.ToDataList();

            // Assert
            Assert.That(!result.Any(d => d.Key.StartsWith("TaskCanceledException")));
        }

#if NETCOREAPP3_1_OR_GREATER
        [Test]
        public void CanGenerateDataListFromTaskCanceledExceptionCanceled()
        {
            // Arrange
            var task = Task.FromCanceled(new CancellationToken(true));
            var taskCanceledException = new TaskCanceledException(task);

            // Act
            var result = taskCanceledException.ToDataList();

            // Assert
            Assert.That(result.Any(d => d.Key == "TaskCanceledException.IsCancellationRequested" && d.Value == "True"));
        }
#endif

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
            aggregateException1.Data.Add("outer-key1", "outer-value1");
            var aggregateException2 = new AggregateException(innerException3);
            aggregateException2.Data.Add("outer-key2", "outer-value2");
            var aggregateException3 = new AggregateException(aggregateException1, aggregateException2);
            aggregateException3.Data.Add("outer-key3", "outer-value3");

            // Act
            var result = aggregateException3.ToDataList();

            // Assert
            Assert.That(result.Any(r => r.Key == "Exception.inner-key1" && r.Value == "inner-value1"));
            Assert.That(result.Any(r => r.Key == "Exception.inner-key2" && r.Value == "inner-value2"));
            Assert.That(result.Any(r => r.Key == "Exception.inner-key3" && r.Value == "inner-value3"));
            Assert.That(result.Any(r => r.Key == "AggregateException.outer-key1" && r.Value == "outer-value1"));
            Assert.That(result.Any(r => r.Key == "AggregateException.outer-key2" && r.Value == "outer-value2"));
            Assert.That(result.Any(r => r.Key == "AggregateException.outer-key3" && r.Value == "outer-value3"));
        }

        [Test]
        public void CanHandleNoData() => Assert.That(new Exception().ToDataList(), Is.Not.Null);

        [Test]
        public void CanHandleNullException() => Assert.That(((Exception)null).ToDataList(), Is.Null);

        [Test]
        public void CanGenererateExceptionInspector()
        {
            // Arrange
            ApplicationException applicationException = null;
            try
            {
                new ExceptionThrower().Throw();
            }
            catch (ApplicationException e)
            {
                applicationException = e;
            }

            var wrapper = new FileNotFoundException("wrapper", "TheFile", applicationException);

            var aggregateException = new AggregateException(wrapper, new NullReferenceException("was null"));
            aggregateException.Data.Add("outer-key1", "outer-value1");

            // Act
            var result = aggregateException.ToDataList();

            // Assert
            var inspector = result.FirstOrDefault(r => r.Key == "X-ELMAHIO-EXCEPTIONINSPECTOR");
            Assert.That(inspector, Is.Not.Null);
            var value = inspector.Value;
            Assert.That(!string.IsNullOrWhiteSpace(value));
            Assert.DoesNotThrow(() => JsonConvert.DeserializeObject(value));
            Assert.That(value.Contains("System.ApplicationException"));
            Assert.That(value.Contains("System.IO.FileNotFoundException"));
            Assert.That(value.Contains("System.AggregateException"));
            Assert.That(value.Contains("ExceptionThrower.Throw()"));
            Assert.That(value.Contains("outer-key1"));
            Assert.That(value.Contains("TheFile"));
            Assert.That(value.Contains("was null"));
        }

        [Test]
        public void CanStopAt10NestedLevelsOfExceptions()
        {
            // Arrange
            var exception = new Exception("1",
                new Exception("2",
                    new Exception("3",
                        new Exception("4",
                            new Exception("5",
                                new Exception("6",
                                    new Exception("7",
                                        new Exception("8",
                                            new Exception("9",
                                                new Exception("10",
                                                    new Exception("11")))))))))));

            // Act
            var result = exception.ToDataList();

            // Assert
            var inspector = result.FirstOrDefault(x => x.Key == "X-ELMAHIO-EXCEPTIONINSPECTOR");
            Assert.That(inspector, Is.Not.Null);
            var value = inspector.Value;
            Assert.That(value.Contains("\"Message\":\"1\""));
            Assert.That(!value.Contains("\"Message\":\"11\""));
        }

        private class ExceptionThrower
        {
            public void Throw()
            {
                throw new ApplicationException("Test");
            }
        }
    }
}
