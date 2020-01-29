import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EntrySeederComponent } from './entry-seeder.component';

describe('EntrySeederComponent', () => {
  let component: EntrySeederComponent;
  let fixture: ComponentFixture<EntrySeederComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EntrySeederComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntrySeederComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
