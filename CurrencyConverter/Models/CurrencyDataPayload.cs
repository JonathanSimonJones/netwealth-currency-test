using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.Models
{
    public class CurrencyDataPayload
    {
        public DateTime DateAndTimeFromExternalAPI { get; set; }
        public DateTime DateAndTimeOfQuery { get; set; }
        public string BaseCurrency { get; set; }
        public FXRate[] CurrencyAndRate {get;set;}
    }

    public class FXRate
    {
        public string Id { get; set; }

        public double Number { get; set; }
    }
}
