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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if(req.Query.Count == 0)
            {
                var currencyConverterService = new CurrencyConverter.CurrencyConverterService();

                try
                {
                    return await currencyConverterService.GetAllCurrenciesJson();
                }
                catch(Exception e)
                {
                    log.LogInformation($"Could not get currency data. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
                }

                return UnableToProcessRequest();
            }
            else
            {
                var currencyConverterService = new CurrencyConverter.CurrencyConverterService();

                if (string.IsNullOrEmpty(req.Query["base"]) != true && 
                    string.IsNullOrEmpty(req.Query["desired"]) != true &&
                    string.IsNullOrEmpty(req.Query["amount"]) != true)
                {
                    try
                    {
                        return await currencyConverterService.ConvertValue(req.Query["base"], req.Query["desired"], req.Query["amount"]);
                    }
                    catch(Exception e)
                    {
                        log.LogInformation($"Could not convert value. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
                    }
                }
                else if(string.IsNullOrEmpty(req.Query["base"]) != true)
                {
                    try
                    {
                        return await currencyConverterService.GetExchangeRatesForCurrency(req.Query["base"]);
                    }
                    catch (Exception e)
                    {
                        log.LogInformation($"Could not get currency data. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
                    }
                }

                return UnableToProcessRequest();
            }
        }

        private static HttpResponseMessage UnableToProcessRequest()
        {
            var myObj = new { response = "Could not process request" };
            var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(myObj);

            return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
