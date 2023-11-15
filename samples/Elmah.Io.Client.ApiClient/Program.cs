using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elmah.Io.Client.ApiClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string apiKey = "API_KEY";
            const string logId = "LOG_ID";

            var client = new HttpClient();

            Console.WriteLine("Logging a new error...");
            var json = JsonConvert.SerializeObject(new
            {
                title = "This is a test message",
                application = "ApiClient.V3",
                detail = "This is a very long description telling more details about this message",
            });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://api.elmah.io/v3/messages/{logId}?api_key={apiKey}", stringContent);

            var location = response.Headers.Location;

            Console.WriteLine($"Successfully logged: {location}");
            Console.WriteLine("Loading the error...");

            var request = new HttpRequestMessage();
            request.RequestUri = location;
            // API key can also be added as a header
            request.Headers.Add("api_key", apiKey);
            response = await client.SendAsync(request);

            var fullErrorJson = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Successfully loaded: {fullErrorJson}");

            Console.WriteLine("Loading errors...");
            response = await client.GetAsync($"https://api.elmah.io/v3/messages/{logId}?api_key={apiKey}");

            string errorsJson = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Successfully loaded: {errorsJson}");
            Console.ReadLine();
        }
    }
}
