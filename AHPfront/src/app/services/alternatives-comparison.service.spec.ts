import { TestBed } from '@angular/core/testing';

import { AlternativesComparisonService } from './alternatives-comparison.service';

describe('AlternativesComparisonService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AlternativesComparisonService = TestBed.get(AlternativesComparisonService);
    expect(service).toBeTruthy();
  });
});
