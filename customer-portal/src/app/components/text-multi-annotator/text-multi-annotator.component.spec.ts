import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextMultiAnnotatorComponent } from './text-multi-annotator.component';

describe('TextMultiAnnotatorComponent', () => {
  let component: TextMultiAnnotatorComponent;
  let fixture: ComponentFixture<TextMultiAnnotatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextMultiAnnotatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextMultiAnnotatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
