import { TestBed } from '@angular/core/testing';

import { CriteriaComparisonService } from './criteria-comparison.service';

describe('CriteriaComparisonService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CriteriaComparisonService = TestBed.get(CriteriaComparisonService);
    expect(service).toBeTruthy();
  });
});
