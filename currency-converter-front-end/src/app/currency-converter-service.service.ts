import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

// export interface CurrencyAndRate {
//     JPY: number;
//     CNY: number;
//     CHF: number;
//     CAD: number;
//     MXN: number;
//     INR: number;
//     BRL: number;
//     RUB: number;
//     KRW: number;
//     IDR: number;
//     TRY: number;
//     SAR: number;
//     SEK: number;
//     NGN: number;
//     PLN: number;
//     ARS: number;
//     NOK: number;
//     TWD: number;
//     IRR: number;
//     AED: number;
//     COP: number;
//     THB: number;
//     ZAR: number;
//     DKK: number;
//     MYR: number;
//     SGD: number;
//     ILS: number;
//     HKD: number;
//     EGP: number;
//     PHP: number;
//     CLP: number;
//     PKR: number;
//     IQD: number;
//     DZD: number;
//     KZT: number;
//     QAR: number;
//     CZK: number;
//     PEN: number;
//     RON: number;
//     VND: number;
//     BDT: number;
//     HUF: number;
//     UAH: number;
//     AOA: number;
//     MAD: number;
//     OMR: number;
//     CUC: number;
//     BYR: number;
//     AZN: number;
//     LKR: number;
//     SDG: number;
//     SYP: number;
//     MMK: number;
//     DOP: number;
//     UZS: number;
//     KES: number;
//     GTQ: number;
//     URY: number;
//     HRV: number;
//     MOP: number;
//     ETB: number;
//     CRC: number;
//     TZS: number;
//     TMT: number;
//     TND: number;
//     PAB: number;
//     LBP: number;
//     RSD: number;
//     LYD: number;
//     GHS: number;
//     YER: number;
//     BOB: number;
//     BHD: number;
//     CDF: number;
//     PYG: number;
//     UGX: number;
//     SVC: number;
//     TTD: number;
//     AFN: number;
//     NPR: number;
//     HNL: number;
//     BIH: number;
//     BND: number;
//     ISK: number;
//     KHR: number;
//     GEL: number;
//     MZN: number;
//     BWP: number;
//     PGK: number;
//     JMD: number;
//     XAF: number;
//     NAD: number;
//     ALL: number;
//     SSP: number;
//     MUR: number;
//     MNT: number;
//     NIO: number;
//     LAK: number;
//     MKD: number;
//     AMD: number;
//     MGA: number;
//     XPF: number;
//     TJS: number;
//     HTG: number;
//     BSD: number;
//     MDL: number;
//     RWF: number;
//     KGS: number;
//     GNF: number;
//     SRD: number;
//     SLL: number;
//     XOF: number;
//     MWK: number;
//     FJD: number;
//     ERN: number;
//     SZL: number;
//     GYD: number;
//     BIF: number;
//     KYD: number;
//     MVR: number;
//     LSL: number;
//     LRD: number;
//     CVE: number;
//     DJF: number;
//     SCR: number;
//     SOS: number;
//     GMD: number;
//     KMF: number;
//     STD: number;
//     XRP: number;
//     AUD: number;
//     BGN: number;
//     BTC: number;
//     JOD: number;
//     GBP: number;
//     ETH: number;
//     EUR: number;
//     LTC: number;
//     NZD: number;
// }

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

  constructor(private http: HttpClient) { 
  }

  testurl = 'http://localhost:7071/api/Currency';

  getTestUrl() {
    return this.http.get<RootObject>(this.testurl);
  }

  getResponse(currencyId : string) {
    //let urlSearchParams = new URLSearchParams();
    //urlSearchParams.append('base', currencyId);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let params = new HttpParams().set("base",currencyId);
    return this.http.get<RootObject>(this.testurl, { params: params});
  }
}
