import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

export interface CurrencyAndRate {
  Id: string;
  Number: number;
}

export interface CurrencyConverterAPIData {
  DateAndTimeFromExternalAPI: Date;
  DateAndTimeOfQuery: Date;
  BaseCurrency: string;
  CurrencyAndRate: CurrencyAndRate[];
}

@Injectable({
  providedIn: 'root'
})

export class CurrencyConverterService {

  /* START Variables */
  
  //private currencyAPI = 'http://localhost:7071/api/Currency';
  //private currencyAPI = "https://currency-api-backend20220101133545.azurewebsites.net/api/Currency";
  private currencyAPI = environment.endpointurl;
  /* END Variables */



  /* START Methods */
  constructor(private http: HttpClient) { 
    console.log(`Api endpoint: ${this.currencyAPI}`);
  }

  getCurrencyData(): Observable<CurrencyConverterAPIData> {
    return this.http.get<CurrencyConverterAPIData>(this.currencyAPI);
  }

  getCurrencyDataGivenACurrency(currencyId : string) : Observable<CurrencyConverterAPIData> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let params = new HttpParams().set("base",currencyId);
    return this.http.get<CurrencyConverterAPIData>(this.currencyAPI, { params: params});
  }

  /* END Methods */
}
