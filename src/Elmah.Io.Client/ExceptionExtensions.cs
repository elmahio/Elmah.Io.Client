using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elmah.Io.Client.Models;

namespace Elmah.Io.Client
{
    public static class ExceptionExtensions
    {
        public static List<Item> ToDataList(this Exception exception)
        {
            if (exception == null) return null;

            var result = new List<Item>();
            var e = exception;
            while (e != null)
            {
                var data = e
                    .Data
                    .Keys
                    .Cast<object>()
                    .Where(k => !string.IsNullOrWhiteSpace(k.ToString()))
                    .Select(k => new Item { Key = k.ToString(), Value = Value(e.Data, k) })
                    .ToList();
                if (data != null && data.Count > 0)
                {
                    result.AddRange(data);
                }

                e = e.InnerException;
            }

            return result;
        }

        private static string Value(IDictionary data, object key)
        {
            var value = data[key];
            if (value == null) return string.Empty;
            return value.ToString();
        }
    }
}