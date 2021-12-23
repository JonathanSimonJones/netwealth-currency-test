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
using System.Text;

namespace currency_api_backend
{



    public static class Currency
    {
        // Create a single, static HttpClient
        private static HttpClient httpClient = new HttpClient();

        [FunctionName("Currency")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var currencyConverterService = new CurrencyConverter.CurrencyConverterService();

            var result = await currencyConverterService.GetAllCurrencies();

            var allCurrenciesAsText = new StringBuilder();

            foreach(var currency in result)
            {
                allCurrenciesAsText.AppendLine(currency.Name);
            }

            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(allCurrenciesAsText.ToString());
        }
    }
}
