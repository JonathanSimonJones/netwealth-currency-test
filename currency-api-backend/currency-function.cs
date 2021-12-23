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
using System.Net;

namespace currency_api_backend
{
    public static class Currency
    {

        [FunctionName("Currency")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if(req.Query.Count == 0)
            {
                var currencyConverterService = new CurrencyConverter.CurrencyConverterService();

                return await currencyConverterService.GetAllCurrenciesJson();
            }
            else
            {
                var currencyConverterService = new CurrencyConverter.CurrencyConverterService();

                return await currencyConverterService.GetExchangeRatesForCurrency(req.Query["base"]);

                //var myObj = new { response = "there was a query" };
                //var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(myObj);

                //return new HttpResponseMessage(HttpStatusCode.OK)
                //{
                //    Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                //};
            }



            //var allCurrenciesAsText = new StringBuilder();
            //
            //foreach (var currency in result)
            //{
            //    allCurrenciesAsText.AppendLine(currency.Name);
            //}
            //
            //return new OkObjectResult(allCurrenciesAsText.ToString());



            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult("testing");
        }
    }
}
