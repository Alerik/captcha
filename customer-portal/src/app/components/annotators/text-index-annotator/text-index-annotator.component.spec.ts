import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextIndexAnnotatorComponent } from './text-index-annotator.component';

describe('TextIndexAnnotatorComponent', () => {
  let component: TextIndexAnnotatorComponent;
  let fixture: ComponentFixture<TextIndexAnnotatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextIndexAnnotatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextIndexAnnotatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
