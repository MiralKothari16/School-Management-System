import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttndenceDashboardComponent } from './attndence-dashboard.component';

describe('AttndenceDashboardComponent', () => {
  let component: AttndenceDashboardComponent;
  let fixture: ComponentFixture<AttndenceDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttndenceDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttndenceDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
