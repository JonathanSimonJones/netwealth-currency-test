using CurrencyConverter.DataSources;
using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    public class CurrencyConverterService
    {
        public async Task<List<Currency>> GetAllCurrencies()
        {
            var exchangerateapi = new ExchangeRatesAPI();

            return await exchangerateapi.GetAllCurrencies();

        }
    }
}
