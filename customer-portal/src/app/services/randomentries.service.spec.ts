import { TestBed } from '@angular/core/testing';

import { RandomentriesService } from './randomentries.service';

describe('RandomentriesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RandomentriesService = TestBed.get(RandomentriesService);
    expect(service).toBeTruthy();
  });
});
