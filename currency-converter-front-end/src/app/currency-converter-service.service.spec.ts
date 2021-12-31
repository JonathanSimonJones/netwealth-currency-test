import { TestBed } from '@angular/core/testing';

import { CurrencyConverterService } from './currency-converter-service.service';

describe('CurrencyConverterServiceService', () => {
  let service: CurrencyConverterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurrencyConverterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
