import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordattendenceComponent } from './recordattendence.component';

describe('RecordattendenceComponent', () => {
  let component: RecordattendenceComponent;
  let fixture: ComponentFixture<RecordattendenceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecordattendenceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecordattendenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
