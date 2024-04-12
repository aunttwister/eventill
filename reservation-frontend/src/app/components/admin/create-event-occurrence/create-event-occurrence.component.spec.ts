import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEventOccurrenceComponent } from './create-event-occurrence.component';

describe('CreateEventOccurrenceComponent', () => {
  let component: CreateEventOccurrenceComponent;
  let fixture: ComponentFixture<CreateEventOccurrenceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEventOccurrenceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEventOccurrenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
