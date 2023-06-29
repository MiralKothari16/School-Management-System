import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordAttendenceComponent } from './record-attendence.component';

describe('RecordAttendenceComponent', () => {
  let component: RecordAttendenceComponent;
  let fixture: ComponentFixture<RecordAttendenceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecordAttendenceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecordAttendenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
