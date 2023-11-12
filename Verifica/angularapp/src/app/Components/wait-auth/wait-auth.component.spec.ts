import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WaitAuthComponent } from './wait-auth.component';

describe('WaitAuthComponent', () => {
  let component: WaitAuthComponent;
  let fixture: ComponentFixture<WaitAuthComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WaitAuthComponent]
    });
    fixture = TestBed.createComponent(WaitAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
