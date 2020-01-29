import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreationOverviewComponent } from './creation-overview.component';

describe('CreationOverviewComponent', () => {
  let component: CreationOverviewComponent;
  let fixture: ComponentFixture<CreationOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreationOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
