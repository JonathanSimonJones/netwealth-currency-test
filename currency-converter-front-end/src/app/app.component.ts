import { Component, Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { CurrencyConverterService, RootObject, CurrencyAndRate } from './currency-converter-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Hello World';
  payload: RootObject = {} as RootObject;
  loading: boolean = false;
  errorMessage: string = "";
  fromListCountry: string = "";
  secondListCountry: string = "";
  fromCountryNumVal: string = "";
  toCountryNumVal: string = "";

  checkoutForm = this.formBuilder.group({initialCurrency: 'USD'});

  currencyPickerFrom = this.formBuilder.group({country: [null]});

  toCurrency = this.formBuilder.group({});
 
  countries = [
    { id: "USD", name: "United States" }
  ];

  constructor(
    private currencyConverterService: CurrencyConverterService,
    private formBuilder: FormBuilder
  )
  {
    this.currencyConverterService.getTestUrl().subscribe(
      (response) => {                           //next() callback
        console.log('response received')
        this.payload = response; 

        //this.updateSelect();
        this.countries.pop();
        for (let i = 0; i < this.payload.CurrencyAndRate.length; i++) {

          var fooid = response.CurrencyAndRate[i].Id;
          //var ranObj = {};
          //ranObj = ;
          this.countries.push({id: fooid, name: fooid});
        }
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

  updateSelect(){
    this.countries = [];
    console.log("hit update select");
    this.countries.push()
  }

  onSubmit(): void {
    // Process checkout data here
    console.warn('doing');
    //this.checkoutForm.reset();

    this.currencyConverterService.getResponse(this.checkoutForm.value).subscribe(
      (response) => {                           //next() callback
        console.log('response received')
        this.payload = response; 
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

  checkOnChange(): void {
    console.log("select list has changed");

    this.currencyConverterService.getResponse(this.fromListCountry).subscribe(
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
        let foo1 = this.payload.CurrencyAndRate.find( x => x.Id == this.secondListCountry);
        
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

  toCountryChange(): void {
    console.log(`value of second count is : ${this.secondListCountry}`);
  }
}
