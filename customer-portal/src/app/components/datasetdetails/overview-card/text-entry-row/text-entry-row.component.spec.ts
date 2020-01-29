import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextEntryRowComponent } from './text-entry-row.component';

describe('TextEntryRowComponent', () => {
  let component: TextEntryRowComponent;
  let fixture: ComponentFixture<TextEntryRowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextEntryRowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextEntryRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
