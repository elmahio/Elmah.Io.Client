using System;
using System.Net.Http;

namespace Elmah.Io.Client
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class HttpClientHandlerFactory
    {
        private static HttpClientHandler? _instance = null;
        private static DateTimeOffset _initTime = DateTimeOffset.MinValue;
        private static readonly TimeSpan _lifeTime = TimeSpan.FromHours(24);

        public static HttpClientHandler GetHttpClientHandler(ElmahIoOptions options)
        {
            if (DateTimeOffset.UtcNow.Subtract(_initTime) > _lifeTime || _instance == null)
            {
#if NETFRAMEWORK
                // .NET Framework's default SecurityProtocol can be limited to TLS 1.0/1.1 depending on the
                // consuming app's <httpRuntime targetFramework> and OS defaults, which the elmah.io API rejects.
                // Explicitly opt in to TLS 1.2 so this doesn't silently fail to connect.
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
#endif

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

                _initTime = DateTimeOffset.UtcNow;
            }
            return _instance;
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
