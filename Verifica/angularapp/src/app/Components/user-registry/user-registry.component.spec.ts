import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRegistryComponent } from './user-registry.component';

describe('UserRegistryComponent', () => {
  let component: UserRegistryComponent;
  let fixture: ComponentFixture<UserRegistryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserRegistryComponent]
    });
    fixture = TestBed.createComponent(UserRegistryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
