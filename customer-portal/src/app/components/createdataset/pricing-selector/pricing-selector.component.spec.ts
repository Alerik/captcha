import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricingSelectorComponent } from './pricing-selector.component';

describe('PricingSelectorComponent', () => {
  let component: PricingSelectorComponent;
  let fixture: ComponentFixture<PricingSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricingSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricingSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
