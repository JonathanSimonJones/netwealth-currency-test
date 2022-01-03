using CurrencyConverter.DataSources;
using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    public class CurrencyConverterService
    {
        public async Task<HttpResponseMessage> GetAllCurrenciesJson()
        {
            var freeCurrencyAPI = new FreeCurrencyAPI();

            try
            {
                return await freeCurrencyAPI.GetAllCurrenciesJson();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HttpResponseMessage> GetExchangeRatesForCurrency(string baseCurrency)
        {
            var freeCurrencyAPI = new FreeCurrencyAPI();

            try
            {
                return await freeCurrencyAPI.GetCurrencyExchangeRates(baseCurrency);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<HttpResponseMessage> ConvertValue(string baseCurrency, string desiredCurrency, string amount)
        {
            var freeCurrencyAPI = new FreeCurrencyAPI();

            try
            {
                var currencyData = await freeCurrencyAPI.GetCurrencyExchangeRatesAsObject(baseCurrency);

                var desiredCurrencyData = currencyData.data.Where(x => desiredCurrency == x.Key).FirstOrDefault();

                var success = false;
                var message = "";
                var convertedAmountAsString = "";

                if(desiredCurrencyData.Key == desiredCurrency)
                {
                    try
                    {
                        var amountAsNum = Convert.ToDouble(amount);

                        var convertedAmount = amountAsNum * desiredCurrencyData.Value;

                        convertedAmountAsString = convertedAmount.ToString();
                        message = "";
                        success = true;
                    }
                    catch (Exception e)
                    {
                        message = "Error in converting number.";

                    }
                }

                var myObj = new { success = success, message = message, convertedAmount = convertedAmountAsString };
                var jsonToReturn = System.Text.Json.JsonSerializer.Serialize(myObj);

                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
