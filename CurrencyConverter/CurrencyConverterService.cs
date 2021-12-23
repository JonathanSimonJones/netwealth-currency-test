using CurrencyConverter.DataSources;
using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
    }
}
