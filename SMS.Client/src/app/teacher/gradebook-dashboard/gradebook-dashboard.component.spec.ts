import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradebookDashboardComponent } from './gradebook-dashboard.component';

describe('GradebookDashboardComponent', () => {
  let component: GradebookDashboardComponent;
  let fixture: ComponentFixture<GradebookDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradebookDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GradebookDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
