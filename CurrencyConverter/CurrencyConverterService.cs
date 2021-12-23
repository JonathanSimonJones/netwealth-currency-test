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
        public async Task<List<Currency>> GetAllCurrencies()
        {
            var exchangerateapi = new ExchangeRatesAPI();

            try
            {
                return await exchangerateapi.GetAllCurrencies();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HttpResponseMessage> GetAllCurrenciesJson()
        {
            var exchangerateapi = new ExchangeRatesAPI();

            try
            {
                return await exchangerateapi.GetAllCurrenciesJson();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HttpResponseMessage> GetExchangeRatesForCurrency(string baseCurrency)
        {
            var exchangerateapi = new ExchangeRatesAPI();

            try
            {
                return await exchangerateapi.GetCurrencyExchangeRates(baseCurrency);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
