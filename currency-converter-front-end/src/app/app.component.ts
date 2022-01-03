import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { CurrencyConverterService, CurrencyConverterAPIData } from './currency-converter-service.service';
import { Country } from './country';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  title = 'Currency Converter';
  currencyConverterAPIData: CurrencyConverterAPIData = {} as CurrencyConverterAPIData;
  errorMessage: string = "";
  fromCountry: string = "";
  toCountry: string = "";
  fromCountryNumVal: string = "";
  toCountryNumVal: string = "";
  toCountryTransferRate: string = "";
  fromCurrencySymbol: string = "";
  toCurrencySymbol: string = "";
  numberEnteredInvalidErrorMessage: string = "Please enter a numeric value only for conversion. e.g. 15.28, or 6000."
  serviceUnavailableErrorMessage: string = "Sorry this service is currently unavailable. Please check back later.";
  apiBackendMessage: string = "Issue connecting to api backend";
 
  countries: Country[] = [
  ];

  constructor(
    private currencyConverterService: CurrencyConverterService,
  ){}

  ngOnInit(): void {
    this.currencyConverterService.getCurrencyData().subscribe(
      (response) => {                           //next() callback
        console.log('response received')
        this.currencyConverterAPIData = response; 

      },
      (error) => {                              //error() callback
        console.error(this.apiBackendMessage);
        this.errorMessage = this.serviceUnavailableErrorMessage;
      },
      () => {                                   //complete() callback
        // Update countries array
        for (let i = 0; i < this.currencyConverterAPIData.CurrencyAndRate.length; i++) {
          var countryId = this.currencyConverterAPIData.CurrencyAndRate[i].Id;
          this.countries.push({Id: countryId, Name: countryId});
        }

        // Sort countries alphabetically
        this.countries.sort((a, b) => a.Name < b.Name ? -1 : a.Name > b.Name ? 1 : 0);
      }
    );
  }

  //////////////////////////////
  // Handles the processing of from/initial country being changed/selected
  //////////////////////////////
  onFromCountryChange(): void {
    this.currencyConverterService.getCurrencyDataGivenACurrency(this.fromCountry).subscribe(
      (response) => {                           //next() callback
        console.log('response received after change');
        this.currencyConverterAPIData = response; 
      },
      (error) => {                              //error() callback
        console.error(this.apiBackendMessage);
        this.errorMessage = this.serviceUnavailableErrorMessage;
      },
      () => {                                   //complete() callback
        // Set currency symbol
        this.fromCurrencySymbol = this.getCurrencySymbol(this.currencyConverterAPIData.BaseCurrency);

        //////////////////////////
        // Handle if to currency value needs to be updated
        //////////////////////////
        if(this.fromCountry)
        {
          if(Number(this.fromCountryNumVal))
          {
            if(this.toCountry)
            {
              this.errorMessage = "";

              this.toCountryTransferRate = String(this.currencyConverterAPIData.CurrencyAndRate.find(x => x.Id == this.toCountry)?.Number);

              let chosenCurrencyData = this.currencyConverterAPIData.CurrencyAndRate.find( x => x.Id == this.toCountry);
              
              if(chosenCurrencyData)
              {
                this.toCountryNumVal = String(chosenCurrencyData.Number * Number(this.fromCountryNumVal));
              }
            }
          }
        }
      }
    );
  }

  //////////////////////////
  // Handle if to currency value needs to be updated
  //////////////////////////
  baseCurrenyValInput(): void {
    if(this.fromCountryNumVal.trim())
    {
      if(Number(this.fromCountryNumVal))
      {
        this.errorMessage = "";
        let chosenCurrencyData = this.currencyConverterAPIData.CurrencyAndRate.find( x => x.Id == this.toCountry);
        
        if(chosenCurrencyData)
        {
          let convertedNum = chosenCurrencyData.Number * Number(this.fromCountryNumVal);
          this.toCountryNumVal = String(convertedNum);
        }
      }
      else
      {
        this.errorMessage = this.numberEnteredInvalidErrorMessage;
      }
    }
  }

  //////////////////////////////
  // Handles the processing of to/desired country being changed/selected
  //////////////////////////////
  onToCountryChange(): void {
    this.toCountryTransferRate = String(this.currencyConverterAPIData.CurrencyAndRate.find(x => x.Id == this.toCountry)?.Number);

    this.toCurrencySymbol = this.getCurrencySymbol(this.currencyConverterAPIData.CurrencyAndRate.find(x => x.Id == this.toCountry)?.Id);

    if(this.fromCountryNumVal.trim())
    {
      if(Number(this.fromCountryNumVal))
      {
        this.errorMessage = "";
        let chosenCurrencyData = this.currencyConverterAPIData.CurrencyAndRate.find( x => x.Id == this.toCountry);
        
        if(chosenCurrencyData)
        {
          this.toCountryNumVal = String(chosenCurrencyData.Number * Number(this.fromCountryNumVal));
        }
      }
      else
      {
        this.errorMessage = this.numberEnteredInvalidErrorMessage;
      }
    }
    // if desired currency num has a value
    else if(this.toCountryNumVal.trim())
    {
      this.errorMessage = "";
      let chosenCurrencyData = this.currencyConverterAPIData.CurrencyAndRate.find( x => x.Id == this.toCountry);
      
      if(chosenCurrencyData)
      {
        let convertedNum = chosenCurrencyData.Number * Number(this.fromCountryNumVal);
        this.toCountryNumVal = String(convertedNum);
      }
    }
  }

  ////////////////////////////////////////
  // Returns a currency symbol based on a currency code
  ////////////////////////////////////////
  private getCurrencySymbol(currency : string | undefined) : string {
    if(currency == undefined)
    {
      return "";
    }
    return (0).toLocaleString(
      "en-US",
      {
        style: 'currency',
        currency: currency,
        minimumFractionDigits: 0,
        maximumFractionDigits: 0
      }
    ).replace(/\d/g, '').trim();
  }
}
