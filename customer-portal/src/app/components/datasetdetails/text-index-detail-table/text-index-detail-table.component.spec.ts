import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextIndexDetailTableComponent } from './text-index-detail-table.component';

describe('TextIndexDetailTableComponent', () => {
  let component: TextIndexDetailTableComponent;
  let fixture: ComponentFixture<TextIndexDetailTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextIndexDetailTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextIndexDetailTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
