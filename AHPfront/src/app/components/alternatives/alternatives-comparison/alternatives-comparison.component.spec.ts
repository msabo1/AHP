import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlternativesComparisonComponent } from './alternatives-comparison.component';

describe('AlternativesComparisonComponent', () => {
  let component: AlternativesComparisonComponent;
  let fixture: ComponentFixture<AlternativesComparisonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlternativesComparisonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlternativesComparisonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
