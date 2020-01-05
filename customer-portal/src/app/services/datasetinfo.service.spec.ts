import { TestBed } from '@angular/core/testing';

import { DatasetinfoService } from './datasetinfo.service';

describe('DatasetinfoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DatasetinfoService = TestBed.get(DatasetinfoService);
    expect(service).toBeTruthy();
  });
});
