﻿using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace CurrencyConverter.DataSources
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

    public class Symbols
    {
        public string AED { get; set; }
        public string AFN { get; set; }
        public string ALL { get; set; }
        public string AMD { get; set; }
        public string ANG { get; set; }
        public string AOA { get; set; }
        public string ARS { get; set; }
        public string AUD { get; set; }
        public string AWG { get; set; }
        public string AZN { get; set; }
        public string BAM { get; set; }
        public string BBD { get; set; }
        public string BDT { get; set; }
        public string BGN { get; set; }
        public string BHD { get; set; }
        public string BIF { get; set; }
        public string BMD { get; set; }
        public string BND { get; set; }
        public string BOB { get; set; }
        public string BRL { get; set; }
        public string BSD { get; set; }
        public string BTC { get; set; }
        public string BTN { get; set; }
        public string BWP { get; set; }
        public string BYN { get; set; }
        public string BYR { get; set; }
        public string BZD { get; set; }
        public string CAD { get; set; }
        public string CDF { get; set; }
        public string CHF { get; set; }
        public string CLF { get; set; }
        public string CLP { get; set; }
        public string CNY { get; set; }
        public string COP { get; set; }
        public string CRC { get; set; }
        public string CUC { get; set; }
        public string CUP { get; set; }
        public string CVE { get; set; }
        public string CZK { get; set; }
        public string DJF { get; set; }
        public string DKK { get; set; }
        public string DOP { get; set; }
        public string DZD { get; set; }
        public string EGP { get; set; }
        public string ERN { get; set; }
        public string ETB { get; set; }
        public string EUR { get; set; }
        public string FJD { get; set; }
        public string FKP { get; set; }
        public string GBP { get; set; }
        public string GEL { get; set; }
        public string GGP { get; set; }
        public string GHS { get; set; }
        public string GIP { get; set; }
        public string GMD { get; set; }
        public string GNF { get; set; }
        public string GTQ { get; set; }
        public string GYD { get; set; }
        public string HKD { get; set; }
        public string HNL { get; set; }
        public string HRK { get; set; }
        public string HTG { get; set; }
        public string HUF { get; set; }
        public string IDR { get; set; }
        public string ILS { get; set; }
        public string IMP { get; set; }
        public string INR { get; set; }
        public string IQD { get; set; }
        public string IRR { get; set; }
        public string ISK { get; set; }
        public string JEP { get; set; }
        public string JMD { get; set; }
        public string JOD { get; set; }
        public string JPY { get; set; }
        public string KES { get; set; }
        public string KGS { get; set; }
        public string KHR { get; set; }
        public string KMF { get; set; }
        public string KPW { get; set; }
        public string KRW { get; set; }
        public string KWD { get; set; }
        public string KYD { get; set; }
        public string KZT { get; set; }
        public string LAK { get; set; }
        public string LBP { get; set; }
        public string LKR { get; set; }
        public string LRD { get; set; }
        public string LSL { get; set; }
        public string LTL { get; set; }
        public string LVL { get; set; }
        public string LYD { get; set; }
        public string MAD { get; set; }
        public string MDL { get; set; }
        public string MGA { get; set; }
        public string MKD { get; set; }
        public string MMK { get; set; }
        public string MNT { get; set; }
        public string MOP { get; set; }
        public string MRO { get; set; }
        public string MUR { get; set; }
        public string MVR { get; set; }
        public string MWK { get; set; }
        public string MXN { get; set; }
        public string MYR { get; set; }
        public string MZN { get; set; }
        public string NAD { get; set; }
        public string NGN { get; set; }
        public string NIO { get; set; }
        public string NOK { get; set; }
        public string NPR { get; set; }
        public string NZD { get; set; }
        public string OMR { get; set; }
        public string PAB { get; set; }
        public string PEN { get; set; }
        public string PGK { get; set; }
        public string PHP { get; set; }
        public string PKR { get; set; }
        public string PLN { get; set; }
        public string PYG { get; set; }
        public string QAR { get; set; }
        public string RON { get; set; }
        public string RSD { get; set; }
        public string RUB { get; set; }
        public string RWF { get; set; }
        public string SAR { get; set; }
        public string SBD { get; set; }
        public string SCR { get; set; }
        public string SDG { get; set; }
        public string SEK { get; set; }
        public string SGD { get; set; }
        public string SHP { get; set; }
        public string SLL { get; set; }
        public string SOS { get; set; }
        public string SRD { get; set; }
        public string STD { get; set; }
        public string SVC { get; set; }
        public string SYP { get; set; }
        public string SZL { get; set; }
        public string THB { get; set; }
        public string TJS { get; set; }
        public string TMT { get; set; }
        public string TND { get; set; }
        public string TOP { get; set; }
        public string TRY { get; set; }
        public string TTD { get; set; }
        public string TWD { get; set; }
        public string TZS { get; set; }
        public string UAH { get; set; }
        public string UGX { get; set; }
        public string USD { get; set; }
        public string UYU { get; set; }
        public string UZS { get; set; }
        public string VEF { get; set; }
        public string VND { get; set; }
        public string VUV { get; set; }
        public string WST { get; set; }
        public string XAF { get; set; }
        public string XAG { get; set; }
        public string XAU { get; set; }
        public string XCD { get; set; }
        public string XDR { get; set; }
        public string XOF { get; set; }
        public string XPF { get; set; }
        public string YER { get; set; }
        public string ZAR { get; set; }
        public string ZMK { get; set; }
        public string ZMW { get; set; }
        public string ZWL { get; set; }
    }

    public class SymbolsRoot
    {
        public bool success { get; set; }
        public Dictionary<string,string> symbols { get; set; }

        //public Symbols symbols { get; set; }
    }

    public class ExchangeRatesAPI
    {
        private readonly string endpoint = "http://api.exchangeratesapi.io/v1/";
        private readonly string access_key = "?access_key=a86b6a6095ca487f7a481200fd1449ef";
        private readonly string currencyEndpoint;
        private static HttpClient httpClient = new HttpClient();

        public ExchangeRatesAPI()
        {
            currencyEndpoint = endpoint + "symbols" + access_key;
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
                    Debug.WriteLine("pulled content successfully");

                    var result = await System.Text.Json.JsonSerializer.DeserializeAsync<SymbolsRoot>(contentStream);

                    return TransformExchangeRateDataToDomain(result.symbols);
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

        private List<Currency> TransformExchangeRateDataToDomain(IDictionary<string, string> dictionaryOfCurrencies)
        {
            var currencies = new List<Currency>();
            
            foreach(var currency in dictionaryOfCurrencies)
            {
                currencies.Add(new Currency() { Code = currency.Key, Name = currency.Value });
            }

            return currencies;
        }
    }
}
