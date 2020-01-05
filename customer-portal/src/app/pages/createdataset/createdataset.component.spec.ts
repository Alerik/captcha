import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatedatasetComponent } from './createdataset.component';

describe('CreatedatasetComponent', () => {
  let component: CreatedatasetComponent;
  let fixture: ComponentFixture<CreatedatasetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatedatasetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatedatasetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
