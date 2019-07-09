import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCriterionComponent } from './add-criterion.component';

describe('AddCriterionComponent', () => {
  let component: AddCriterionComponent;
  let fixture: ComponentFixture<AddCriterionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCriterionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCriterionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
