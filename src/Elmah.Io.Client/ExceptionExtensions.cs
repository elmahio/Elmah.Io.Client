using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Extension methods provided by elmah.io for the Exception class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Generate a CreateMessage object from an exception. This object can used as a template
        /// and decorated with additional properties before sent to the CreateAndNotify method.
        /// </summary>
        public static CreateMessage ToMessage(this Exception exception)
        {
            if (exception == null) return null;

            var message = new CreateMessage
            {
                DateTime = DateTime.UtcNow,
                Title = exception.Message,
                Severity = "Error",
                Detail = exception.ToString(),
                Data = exception.ToDataList(),
                Type = exception.GetBaseException().GetType().FullName,
            };

            return message;
        }

        /// <summary>
        /// Pulls as much information as possible from an Exception to a list of elmah.io Items.
        /// </summary>
        public static List<Item> ToDataList(this Exception exception)
        {
            if (exception == null) return null;

            var result = exception.Iterate();
            var dataItems = new List<Item>(result.Items)
            {
                new Item("X-ELMAHIO-EXCEPTIONINSPECTOR", JsonConvert.SerializeObject(result.Exception)),
                new Item("X-ELMAHIO-FRAMEWORKDESCRIPTION", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription)
            };
            return dataItems;
        }

        private static IterateExceptionResult Iterate(this Exception exception, int level = 1)
        {
            // Don't iterate more than 10 nested exceptions
            if (level > 10) return null;

            var exceptionModel = new ExceptionModel();
            exceptionModel.Message = exception.Message;
            exceptionModel.Type = exception.GetType().FullName;
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
            exceptionModel.TargetSite = exception.TargetSite?.ToString();
#endif
            exceptionModel.Source = exception.Source;
            exceptionModel.HResult = exception.HResult;
            exceptionModel.StackTrace = exception.StackTrace;
            var result = new List<Item>();

            var input = exception
                .Data
                .Keys
                .Cast<object>()
                .Where(k => !string.IsNullOrWhiteSpace(k.ToString()));

            if (input != null && input.Count() > 0)
            {
                exceptionModel.Data = input.Select(i => new KeyValuePair<string, string>(i.ToString(), Value(exception.Data, i))).ToList();
                result.AddRange(input
                    .Select(k => new Item { Key = exception.ItemName(k.ToString()), Value = Value(exception.Data, k) })
                    .ToList());
            }

            if (!string.IsNullOrWhiteSpace(exception.HelpLink))
            {
                result.Add(new Item { Key = exception.ItemName(nameof(exception.HelpLink)), Value = exception.HelpLink });
                exceptionModel.HelpLink = exception.HelpLink;
            }

            var exceptionSpecificItems = ExceptionSpecificItems(exception, exceptionModel);
            if (exceptionSpecificItems.Count > 0)
            {
                result.AddRange(exceptionSpecificItems);
            }

            if (exception is AggregateException ae)
            {
                foreach (var innerException in ae.InnerExceptions)
                {
                    var innerResult = innerException.Iterate(1 + level);
                    if (innerResult?.Items.Count > 0)
                    {
                        result.AddRange(innerResult.Items);
                    }

                    if (innerResult?.Exception != null)
                    {
                        exceptionModel.Inners.Add(innerResult.Exception);
                    }
                }
            }
            else if (exception.InnerException != null)
            {
                var innerResult = exception.InnerException.Iterate(1 + level);
                if (innerResult?.Items.Count > 0)
                {
                    result.AddRange(innerResult.Items);
                }

                if (innerResult?.Exception != null)
                {
                    exceptionModel.Inners.Add(innerResult.Exception);
                }
            }

            return new IterateExceptionResult
            {
                 Items = result,
                 Exception = exceptionModel,
            };
        }

        /// <summary>
        /// Helper method to extract items from different known exception types and their properties.
        /// </summary>
        private static List<Item> ExceptionSpecificItems(Exception e, ExceptionModel ex)
        {
            var result = new List<Item>();
            if (e is ArgumentException ae)
            {
                if (!string.IsNullOrWhiteSpace(ae.ParamName))
                {
                    result.Add(new Item { Key = ae.ItemName(nameof(ae.ParamName)), Value = ae.ParamName });
                    ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(ae.ParamName), ae.ParamName));
                }
            }

            if (e is BadImageFormatException bife)
            {
                if (!string.IsNullOrWhiteSpace(bife.FileName))
                {
                    result.Add(new Item { Key = bife.ItemName(nameof(bife.FileName)), Value = bife.FileName });
                    ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(bife.FileName), bife.FileName));
                }
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
                if (!string.IsNullOrWhiteSpace(bife.FusionLog))
                {
                    result.Add(new Item { Key = bife.ItemName(nameof(bife.FusionLog)), Value = bife.FusionLog });
                    ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(bife.FusionLog), bife.FusionLog));
                }
#endif
            }

            if (e is TaskCanceledException tce)
            {
                if (tce.CancellationToken != CancellationToken.None)
                {
                    result.Add(new Item { Key = tce.ItemName(nameof(tce.CancellationToken.IsCancellationRequested)), Value = tce.CancellationToken.IsCancellationRequested.ToString() });
                    ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(tce.CancellationToken.IsCancellationRequested), tce.CancellationToken.IsCancellationRequested.ToString()));
                }
            }

            if (e is FileNotFoundException fnfe)
            {
                if (!string.IsNullOrWhiteSpace(fnfe.FileName))
                {
                    result.Add(new Item { Key = fnfe.ItemName(nameof(fnfe.FileName)), Value = fnfe.FileName });
                    ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(fnfe.FileName), fnfe.FileName));
                }
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
                if (!string.IsNullOrWhiteSpace(fnfe.FusionLog))
                {
                    result.Add(new Item { Key = fnfe.ItemName(nameof(fnfe.FusionLog)), Value = fnfe.FusionLog });
                    ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(fnfe.FusionLog), fnfe.FusionLog));
                }
#endif
            }

#if NETSTANDARD1_4 || NETSTANDARD2_0 || NET45 || NET46 || NET461
            if (e is System.Net.Sockets.SocketException se)
            {
                result.Add(new Item { Key = se.ItemName(nameof(se.SocketErrorCode)), Value = se.SocketErrorCode.ToString() });
                ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(se.SocketErrorCode), se.SocketErrorCode.ToString()));
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
                result.Add(new Item { Key = se.ItemName(nameof(se.ErrorCode)), Value = se.ErrorCode.ToString() });
                ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(se.ErrorCode), se.ErrorCode.ToString()));
#endif
            }
#endif

#if NETSTANDARD2_0 || NET45 || NET46 || NET461
            if (e is System.Net.WebException we)
            {
                result.Add(new Item { Key = we.ItemName(nameof(we.Status)), Value = we.Status.ToString() });
                ex.ExceptionSpecific.Add(new KeyValuePair<string, string>(nameof(we.Status), we.Status.ToString()));
            }
#endif

            return result;
        }

        /// <summary>
        /// Generate an item name of a property on an exception.
        /// </summary>
        public static string ItemName(this Exception exception, string propertyName)
        {
            return $"{exception.GetType().Name}.{propertyName}";
        }

        private static string Value(IDictionary data, object key)
        {
            var value = data[key];
            if (value == null) return string.Empty;
            return value.ToString();
        }

        private class ExceptionModel
        {
            public string Type { get; set; }
            public string Message { get; set; }
            public string StackTrace { get; set; }
            public string HelpLink { get; set; }
            public int HResult { get; set; }
            public string TargetSite { get; set; }
            public string Source { get; set; }
            public List<ExceptionModel> Inners { get; set; } = new List<ExceptionModel>();
            public List<KeyValuePair<string, string>> Data { get; set; } = new List<KeyValuePair<string, string>>();
            public List<KeyValuePair<string, string>> ExceptionSpecific { get; set; } = new List<KeyValuePair<string, string>>();
        }

        private class IterateExceptionResult
        {
            public ExceptionModel Exception { get; set; }
            public List<Item> Items { get; set; }
        }
    }
}