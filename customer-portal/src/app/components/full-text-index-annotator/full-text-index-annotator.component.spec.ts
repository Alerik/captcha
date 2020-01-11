import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FullTextIndexAnnotatorComponent } from './full-text-index-annotator.component';

describe('FullTextIndexAnnotatorComponent', () => {
  let component: FullTextIndexAnnotatorComponent;
  let fixture: ComponentFixture<FullTextIndexAnnotatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FullTextIndexAnnotatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FullTextIndexAnnotatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
