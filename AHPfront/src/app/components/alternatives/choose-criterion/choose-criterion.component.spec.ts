import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseCriterionComponent } from './choose-criterion.component';

describe('ChooseCriterionComponent', () => {
  let component: ChooseCriterionComponent;
  let fixture: ComponentFixture<ChooseCriterionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChooseCriterionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseCriterionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
