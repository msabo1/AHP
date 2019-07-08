import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriteriaComparisonsComponent } from './criteria-comparisons.component';

describe('CriteriaComparisonsComponent', () => {
  let component: CriteriaComparisonsComponent;
  let fixture: ComponentFixture<CriteriaComparisonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CriteriaComparisonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriteriaComparisonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
