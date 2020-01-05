import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TextIndexAnnotationComponent } from './text-index-annotation.component';

describe('TextIndexAnnotationComponent', () => {
  let component: TextIndexAnnotationComponent;
  let fixture: ComponentFixture<TextIndexAnnotationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TextIndexAnnotationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TextIndexAnnotationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
