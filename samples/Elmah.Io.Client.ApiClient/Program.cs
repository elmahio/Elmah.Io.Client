using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Elmah.Io.Client.ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string apiKey = "API_KEY";
            const string logId = "LOG_ID";

            Console.WriteLine("Logging a new error...");
            var request = (HttpWebRequest)WebRequest.Create($"https://api.elmah.io/v3/messages/{logId}?api_key={apiKey}");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var createError = new
            {
                title = "This is a test message",
                application = "ApiClient.V3",
                detail = "This is a very long description telling more details about this message",
            };

            var createErrorString = JsonConvert.SerializeObject(createError);
            var bytes = Encoding.UTF8.GetBytes(createErrorString);
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            var outputStream = request.GetRequestStream();
            outputStream.Write(bytes, 0, bytes.Length);

            var response = request.GetResponse();
            var location = response.Headers[HttpResponseHeader.Location];

            Console.WriteLine($"Successfully logged: {location}");
            Console.WriteLine("Loading the error...");

            request = (HttpWebRequest)WebRequest.Create(location);
            request.Method = "GET";
            // API key can also be added as a header
            request.Headers.Add("api_key", apiKey);
            response = request.GetResponse();

            string fullErrorJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                fullErrorJson = streamReader.ReadToEnd();
            }

            Console.WriteLine($"Successfully loaded: {fullErrorJson}");
            Console.WriteLine("Loading errors...");
            request = (HttpWebRequest)WebRequest.Create($"https://api.elmah.io/v3/messages/{logId}?api_key={apiKey}");
            request.Method = "GET";
            response = request.GetResponse();

            string errorsJson;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                errorsJson = streamReader.ReadToEnd();
            }

            Console.WriteLine($"Successfully loaded: {errorsJson}");
            Console.ReadLine();
        }
    }
}
