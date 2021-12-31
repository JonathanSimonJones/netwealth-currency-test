using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using System.Net;

namespace CurrencyConverter.DataSources
{
    public class FreeCurrencyAPI
    {
        private class FreeCurrencyQuery
        {
            public string apikey { get; set; }
            public int timestamp { get; set; }
            public string base_currency { get; set; }
        }

        private class FreeCurrencyRoot
        {
            public FreeCurrencyQuery query { get; set; }
            public Dictionary<string, double> data { get; set; }
        }

        private readonly string endpoint = "https://freecurrencyapi.net/api/v2/";
        private readonly string accessKey = "?apikey=12fee950-6400-11ec-a08f-99a1f80275ce";
        private readonly string currencyEndpoint;
        private static HttpClient httpClient = new HttpClient();
        private readonly string apiName = "freecurrencyapi";

        public FreeCurrencyAPI()
        {
            currencyEndpoint = endpoint + "latest" + accessKey;
        }

        public async Task<FXRate[]> GetAllCurrencies()
        {
            // Construct message
            var httpRequestMessage = new HttpRequestMessage(
                                            HttpMethod.Get,
                                            currencyEndpoint)
            { };

            try
            {
                // Get message details
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, x-client-key, x-client-token, x-client-secret, Authorization");

                // Check for success
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await System.Text.Json.JsonSerializer.DeserializeAsync<FreeCurrencyRoot>(contentStream);

                    return TransformExchangeRateDataToDomain(result).CurrencyAndRate;
                }
                else
                {
                    throw new HttpRequestException($"The {apiName} request did not return a success status code");
                }
            }
            catch(Exception e)
            {
                throw new Exception($"Something went wrong when requesting the data from {apiName}. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
            }

            throw new Exception($"Something went wrong when trying to get all currencies from {apiName}");
        }

        public async Task<HttpResponseMessage> GetAllCurrenciesJson()
        {
            // Construct message
            var httpRequestMessage = new HttpRequestMessage(
                                            HttpMethod.Get,
                                            currencyEndpoint)
            { };

            try
            {
                // Get message details
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, x-client-key, x-client-token, x-client-secret, Authorization");

                // Check for success
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await System.Text.Json.JsonSerializer.DeserializeAsync<FreeCurrencyRoot>(contentStream);

                    var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(TransformExchangeRateDataToDomain(result));

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                    };
                }
                else
                {
                    throw new HttpRequestException($"The {apiName} request did not return a success status code");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Something went wrong when requesting the data from {apiName}. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
            }


            throw new Exception();
        }

        private CurrencyDataPayload TransformExchangeRateDataToDomain(FreeCurrencyRoot dataFromAPI)
        {
            var currencyDataPayload = new CurrencyDataPayload();

            currencyDataPayload.DateAndTimeFromExternalAPI = UnixTimeStampToDateTime(dataFromAPI.query.timestamp);
            currencyDataPayload.DateAndTimeOfQuery = DateTime.Now;
            currencyDataPayload.BaseCurrency = dataFromAPI.query.base_currency;
            currencyDataPayload.CurrencyAndRate = new FXRate[dataFromAPI.data.Count];

            var i = 0;
            foreach(var currency in dataFromAPI.data)
            {
                currencyDataPayload.CurrencyAndRate[i] = new FXRate() { Id = currency.Key, Number = Convert.ToDouble(currency.Value)};
                i++;
            }

            // Add USD if missing
            if(currencyDataPayload.CurrencyAndRate.Select(x => x.Id == "USD").FirstOrDefault() == false)
            {
                var updatedCurrencyAndRate = new List<FXRate>();
                updatedCurrencyAndRate.AddRange(currencyDataPayload.CurrencyAndRate);
                updatedCurrencyAndRate.Add(new FXRate() { Id = "USD", Number = 1 });
                currencyDataPayload.CurrencyAndRate = updatedCurrencyAndRate.ToArray();
            }

            return currencyDataPayload;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public async Task<HttpResponseMessage> GetCurrencyExchangeRates(string baseCurrency)
        {
            var currencies = await GetAllCurrencies();
            foreach (var currency in currencies)
            {
                // Check the currency can be converted 
                if(currency.Id == baseCurrency)
                {
                    // Construct message
                    var httpRequestMessage = new HttpRequestMessage(
                                                    HttpMethod.Get,
                                                    currencyEndpoint + "&base_currency=" + baseCurrency)
                    { };

                    try
                    {
                        // Get message details
                        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                        //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                        //httpResponseMessage.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
                        //httpResponseMessage.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, x-client-key, x-client-token, x-client-secret, Authorization");

                        // Check for success
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            //return httpResponseMessage;

                            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                            var result = await System.Text.Json.JsonSerializer.DeserializeAsync<FreeCurrencyRoot>(contentStream);

                            var content = System.Text.Json.JsonSerializer.Serialize(TransformExchangeRateDataToDomain(result));

                            return new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new StringContent(content, Encoding.UTF8, "application/json")
                            };
                        }
                        else
                        {
                            throw new HttpRequestException($"The {apiName} request did not return a success status code");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Something went wrong when requesting the data from {apiName}. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
                    }
                }
            }

            var myObj = new { response = "Could not find the currency requested" };
            var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(myObj);

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };

            throw new NotImplementedException();
        }
    }
}
