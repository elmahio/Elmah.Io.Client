using Azure.Core;
using Azure.Core.Pipeline;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Elmah.Io.Client
{
    public class ApiKeyCredentials : HttpClient
    {
        public ApiKeyCredentials(string apiKey)
        {
            DefaultRequestHeaders.Remove("api_key");
            DefaultRequestHeaders.Add("api_key", apiKey);
        }
    }
}