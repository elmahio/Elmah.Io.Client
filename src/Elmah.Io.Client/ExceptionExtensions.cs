using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elmah.Io.Client.Models;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Extension methods provided by elmah.io for the Exception class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Pulls as much information as possible from an Exception to a list of elmah.io Items.
        /// </summary>
        public static List<Item> ToDataList(this Exception exception)
        {
            if (exception == null) return null;

            var result = exception.Iterate();
            return result;
        }

        private static List<Item> Iterate(this Exception exception)
        {
            var result = new List<Item>();
            if (exception is AggregateException ae)
            {
                if (ae.InnerExceptions != null)
                {
                    foreach (var innerException in ae.InnerExceptions)
                    {
                        var innerResult = innerException.Iterate();
                        if (innerResult.Count > 0)
                        {
                            result.AddRange(innerResult);
                        }
                    }
                }
            }
            else
            {
                var exceptionName = exception.GetType().Name;
                var data = exception
                    .Data
                    .Keys
                    .Cast<object>()
                    .Where(k => !string.IsNullOrWhiteSpace(k.ToString()))
                    .Select(k => new Item { Key = exception.ItemName(k.ToString()), Value = Value(exception.Data, k) })
                    .ToList();

                if (data != null && data.Count > 0)
                {
                    result.AddRange(data);
                }

                if (!string.IsNullOrWhiteSpace(exception.HelpLink))
                {
                    result.Add(new Item { Key = exception.ItemName(nameof(exception.HelpLink)), Value = exception.HelpLink });
                }

                var exceptionSpecificItems = ExceptionSpecificItems(exception);
                if (exceptionSpecificItems.Count > 0)
                {
                    result.AddRange(exceptionSpecificItems);
                }

                if (exception.InnerException != null)
                {
                    var innerResult = exception.InnerException.Iterate();
                    if (innerResult.Count > 0)
                    {
                        result.AddRange(innerResult);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Helper method to extract items from different known exception types and their properties.
        /// </summary>
        private static List<Item> ExceptionSpecificItems(Exception e)
        {
            var result = new List<Item>();
            if (e is ArgumentException ae)
            {
                if (!string.IsNullOrWhiteSpace(ae.ParamName))
                    result.Add(new Item { Key = ae.ItemName(nameof(ae.ParamName)), Value = ae.ParamName });
            }

            if (e is BadImageFormatException bife)
            {
                if (!string.IsNullOrWhiteSpace(bife.FileName))
                    result.Add(new Item { Key = bife.ItemName(nameof(bife.FileName)), Value = bife.FileName });
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
                if (!string.IsNullOrWhiteSpace(bife.FusionLog))
                    result.Add(new Item { Key = bife.ItemName(nameof(bife.FusionLog)), Value = bife.FusionLog });
#endif
            }

            if (e is FileNotFoundException fnfe)
            {
                if (!string.IsNullOrWhiteSpace(fnfe.FileName))
                    result.Add(new Item { Key = fnfe.ItemName(nameof(fnfe.FileName)), Value = fnfe.FileName });
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
                if (!string.IsNullOrWhiteSpace(fnfe.FusionLog))
                    result.Add(new Item { Key = fnfe.ItemName(nameof(fnfe.FusionLog)), Value = fnfe.FusionLog });
#endif
            }

#if NETSTANDARD1_4 || NETSTANDARD2_0 || NET45 || NET46 || NET461
            if (e is System.Net.Sockets.SocketException se)
            {
                result.Add(new Item { Key = se.ItemName(nameof(se.SocketErrorCode)), Value = se.SocketErrorCode.ToString() });
#if NETSTANDARD2_0 || NET45 || NET46 || NET461
                result.Add(new Item { Key = se.ItemName(nameof(se.ErrorCode)), Value = se.ErrorCode.ToString() });
#endif
            }
#endif

#if NETSTANDARD2_0 || NET45 || NET46 || NET461
            if (e is System.Net.WebException we)
            {
                string ss = ItemName(we, nameof(we.Status));
                result.Add(new Item { Key = we.ItemName(nameof(we.Status)), Value = we.Status.ToString() });
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
    }
}