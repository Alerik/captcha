import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextIndexEntryDetailsComponent } from './text-index-entry-details.component';

describe('TextIndexEntryDetailsComponent', () => {
  let component: TextIndexEntryDetailsComponent;
  let fixture: ComponentFixture<TextIndexEntryDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextIndexEntryDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextIndexEntryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
