import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppRegistryComponent } from './app-registry.component';

describe('AppRegistryComponent', () => {
  let component: AppRegistryComponent;
  let fixture: ComponentFixture<AppRegistryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppRegistryComponent]
    });
    fixture = TestBed.createComponent(AppRegistryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
