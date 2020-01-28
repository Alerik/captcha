import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FullTextMultiAnnotatorComponent } from './full-text-multi-annotator.component';

describe('FullTextMultiAnnotatorComponent', () => {
  let component: FullTextMultiAnnotatorComponent;
  let fixture: ComponentFixture<FullTextMultiAnnotatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FullTextMultiAnnotatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FullTextMultiAnnotatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
