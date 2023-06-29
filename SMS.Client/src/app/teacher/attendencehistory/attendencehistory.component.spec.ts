import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendencehistoryComponent } from './attendencehistory.component';

describe('AttendencehistoryComponent', () => {
  let component: AttendencehistoryComponent;
  let fixture: ComponentFixture<AttendencehistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttendencehistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttendencehistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
