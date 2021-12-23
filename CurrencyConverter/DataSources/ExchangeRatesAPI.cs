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
    public class ExchangeRateRoot
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Dictionary<string, string> rates { get; set; }
    }

    public class SymbolsRoot
    {
        public bool success { get; set; }
        public Dictionary<string,string> symbols { get; set; }
    }

    public class Query
    {
        public string apikey { get; set; }
        public int timestamp { get; set; }
        public string base_currency { get; set; }
    }

    public class Root
    {
        public Query query { get; set; }
        public Dictionary<string, double> data { get; set; }
    }

    public class ExchangeRatesAPI
    {
        //private readonly string endpoint = "http://api.exchangeratesapi.io/v1/";
        //private readonly string access_key = "?access_key=a86b6a6095ca487f7a481200fd1449ef";
        //private readonly string endpoint = "http://data.fixer.io/api/";
        //private readonly string access_key = "?access_key=c9394fc1aef428ca042d0bb6c008f73b";
        private readonly string endpoint = "https://freecurrencyapi.net/api/v2/";
        private readonly string access_key = "?apikey=12fee950-6400-11ec-a08f-99a1f80275ce";
        private readonly string currencyEndpoint;
        private readonly string exchangeRateEndpoint;
        private static HttpClient httpClient = new HttpClient();

        public ExchangeRatesAPI()
        {
            currencyEndpoint = endpoint + "latest" + access_key;
            exchangeRateEndpoint = endpoint + "latest" + access_key;
        }

        public async Task<List<Currency>> GetAllCurrencies()
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

                // Check for success
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await System.Text.Json.JsonSerializer.DeserializeAsync<Root>(contentStream);

                    return TransformExchangeRateDataToDomain(result.data);
                }
                else
                {
                    throw new HttpRequestException("The ExchangeRatesAPI request did not return a success status code");
                }
            }
            catch(Exception e)
            {
                throw new Exception($"Something went wrong when requesting the data from ExchangeRatesAPI. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
            }

            


            throw new NotImplementedException();
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

                // Check for success
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var result = await System.Text.Json.JsonSerializer.DeserializeAsync<Root>(contentStream);

                    var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(result.data);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                    };
                }
                else
                {
                    throw new HttpRequestException("The ExchangeRatesAPI request did not return a success status code");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Something went wrong when requesting the data from ExchangeRatesAPI. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
            }


            throw new Exception();
        }

        private List<Currency> TransformExchangeRateDataToDomain(IDictionary<string, double> dictionaryOfCurrencies)
        {
            var currencies = new List<Currency>();
            
            foreach(var currency in dictionaryOfCurrencies)
            {
                currencies.Add(new Currency() { Code = currency.Key });
            }

            return currencies;
        }

        public async Task<HttpResponseMessage> GetCurrencyExchangeRates(string baseCurrency)
        {
            var currencies = await GetAllCurrencies();
            foreach (var currency in currencies)
            {
                // Check the currency can be converted 
                if(currency.Code == baseCurrency)
                {
                    // Construct message
                    var httpRequestMessage = new HttpRequestMessage(
                                                    HttpMethod.Get,
                                                    exchangeRateEndpoint + "&baseCurrency=" + baseCurrency)
                    { };

                    try
                    {
                        // Get message details
                        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                        // Check for success
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            return httpResponseMessage;

                            //var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                            //var result = await System.Text.Json.JsonSerializer.DeserializeAsync<ExchangeRateRoot>(contentStream);

                            //var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(result.rates);

                            //return new HttpResponseMessage(HttpStatusCode.OK)
                            //{
                            //    Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                            //};
                        }
                        else
                        {
                            throw new HttpRequestException("The ExchangeRatesAPI request did not return a success status code");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Something went wrong when requesting the data from ExchangeRatesAPI. Inner: {e.InnerException}. Message: {e.Message}. Stack trace: {e.StackTrace}");
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
