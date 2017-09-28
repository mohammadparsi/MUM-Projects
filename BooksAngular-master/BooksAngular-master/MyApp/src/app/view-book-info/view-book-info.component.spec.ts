import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewBookInfoComponent } from './view-book-info.component';

describe('ViewBookInfoComponent', () => {
  let component: ViewBookInfoComponent;
  let fixture: ComponentFixture<ViewBookInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewBookInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewBookInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
