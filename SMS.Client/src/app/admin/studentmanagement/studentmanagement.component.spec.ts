import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentmanagementComponent } from './studentmanagement.component';

describe('StudentmanagementComponent', () => {
  let component: StudentmanagementComponent;
  let fixture: ComponentFixture<StudentmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentmanagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
