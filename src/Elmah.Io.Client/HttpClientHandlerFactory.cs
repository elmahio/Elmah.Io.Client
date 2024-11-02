using System;
using System.Net.Http;

namespace Elmah.Io.Client
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class HttpClientHandlerFactory
    {
        private static HttpClientHandler _instance = null;
        private static DateTime _initTime = DateTime.MinValue;
        private static readonly TimeSpan _lifeTime = TimeSpan.FromHours(24);

        public static HttpClientHandler GetHttpClientHandler(ElmahIoOptions options)
        {
            if (DateTime.Now.Subtract(_initTime) > _lifeTime || _instance == null)
            {
                if (options.WebProxy != null)
                {
                    _instance = new HttpClientHandler
                    {
                        UseProxy = true,
                        Proxy = options.WebProxy,
                    };
                }
                else
                {
                    _instance = new HttpClientHandler
                    {
                        UseProxy = false,
                    };
                }
                _initTime = DateTime.Now;
            }
            return _instance;
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
