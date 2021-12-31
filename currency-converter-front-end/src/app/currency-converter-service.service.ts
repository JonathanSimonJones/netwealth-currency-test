import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

export interface CurrencyAndRate {
  Id: string;
  Number: number;
}

export interface RootObject {
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
  
  private currencyAPI = 'http://localhost:7071/api/Currency';
  
  /* END Variables */



  /* START Methods */
  constructor(private http: HttpClient) { 
  }

  getTestUrl(): Observable<RootObject> {
    return this.http.get<RootObject>(this.currencyAPI);
  }

  getResponse(currencyId : string) : Observable<RootObject> {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let params = new HttpParams().set("base",currencyId);
    return this.http.get<RootObject>(this.currencyAPI, { params: params});
  }

  /* END Methods */
}
