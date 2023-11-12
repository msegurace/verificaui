import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormatDatePipe } from './format-date.pipe';

describe('FormatDatePipe', () => {
  let component: FormatDatePipe;
  let fixture: ComponentFixture<FormatDatePipe>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormatDatePipe]
    });
    fixture = TestBed.createComponent(FormatDatePipe);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
