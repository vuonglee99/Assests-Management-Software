import { TestBed, inject } from '@angular/core/testing';

import { ExcelExportService } from './excelexport.service';

describe('ExcelExportService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ExcelExportService]
    });
  });

  it('should be created', inject([ExcelExportService], (service: ExcelExportService) => {
    expect(service).toBeTruthy();
  }));
});
