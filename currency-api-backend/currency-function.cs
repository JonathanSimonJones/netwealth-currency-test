using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace currency_api_backend
{
    public class Rates
    {
        public double USD { get; set; }
        public double AUD { get; set; }
        public double CAD { get; set; }
        public double PLN { get; set; }
        public double MXN { get; set; }
    }

    public class Root
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }


    public static class Currency
    {
        // Create a single, static HttpClient
        private static HttpClient httpClient = new HttpClient();

        [FunctionName("Currency")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "http://api.exchangeratesapi.io/v1/latest?access_key=a86b6a6095ca487f7a481200fd1449ef")
            {};

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                try
                {
                    var result = await System.Text.Json.JsonSerializer.DeserializeAsync<Root>(contentStream);
                    log.LogInformation("worked");
                }
                catch(Exception e)
                {
                    log.LogInformation(e.InnerException.ToString() + "\n" + e.Message);
                }

                log.LogInformation("finished.");
            }

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
