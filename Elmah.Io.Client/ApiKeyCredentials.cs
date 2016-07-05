using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Elmah.Io.Client
{
    public class ApiKeyCredentials : ServiceClientCredentials
    {
        private readonly string _apiKey;

        public ApiKeyCredentials(string apiKey)
        {
            _apiKey = apiKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Remove("api_key");
            request.Headers.TryAddWithoutValidation("api_key", _apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}