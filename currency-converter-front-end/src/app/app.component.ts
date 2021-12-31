import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { CurrencyConverterService, RootObject, CurrencyAndRate } from './currency-converter-service.service';
import { Country } from './country';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  title = 'Currency Converter';
  payload: RootObject = {} as RootObject;
  loading: boolean = false;
  errorMessage: string = "";
  fromCountry: string = "";
  toCountry: string = "";
  fromCountryNumVal: string = "";
  toCountryNumVal: string = "";
  toCountryTransferRate: string = "";
 
  countries: Country[] = [
  ];

  constructor(
    private currencyConverterService: CurrencyConverterService,
  ){}

  ngOnInit(): void {
    this.currencyConverterService.getTestUrl().subscribe(
      (response) => {                           //next() callback
        console.log('response received')
        this.payload = response; 
        for (let i = 0; i < this.payload.CurrencyAndRate.length; i++) {
          var fooid = response.CurrencyAndRate[i].Id;
          this.countries.push({Id: fooid, Name: fooid});
        }
        this.countries.sort((a, b) => a.Name < b.Name ? -1 : a.Name > b.Name ? 1 : 0);
      },
      (error) => {                              //error() callback
        console.error('Request failed with error')
        this.errorMessage = error;
        this.loading = false;
      },
      () => {                                   //complete() callback
        //console.error('Request completed')      //This is actually not needed 
        this.loading = false; 
      }
    );
  }

  onFromCountryChange(): void {
    this.currencyConverterService.getResponse(this.fromCountry).subscribe(
      (response) => {                           //next() callback
        console.log('response received after change')
        this.payload = response; 
        console.log(response.BaseCurrency)
      },
      (error) => {                              //error() callback
        console.error('Request failed with error')
        this.errorMessage = error;
        this.loading = false;
      },
      () => {                                   //complete() callback
        //console.error('Request completed')      //This is actually not needed 
        this.loading = false; 
      }
    );
  }

  baseCurrenyValChange(): void {
    if(this.fromCountryNumVal.trim())
    {
      if(Number(this.fromCountryNumVal))
      {
        this.errorMessage = "";
        let foo1 = this.payload.CurrencyAndRate.find( x => x.Id == this.toCountry);
        
        if(foo1)
        {
          let convertedNum = foo1.Number * Number(this.fromCountryNumVal);
          this.toCountryNumVal = String(convertedNum);
        }
      }
      else
      {
        this.errorMessage = "From country does not contain a number";
      }
    }
  }

  onToCountryChange(): void {
    this.toCountryTransferRate = String(this.payload.CurrencyAndRate.find(x => x.Id == this.toCountry)?.Number);
  }
}
