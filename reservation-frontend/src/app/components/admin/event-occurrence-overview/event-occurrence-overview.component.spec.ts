import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventOccurrenceOverviewComponent } from './event-occurrence-overview.component';

describe('EventOccurrenceOverviewComponent', () => {
  let component: EventOccurrenceOverviewComponent;
  let fixture: ComponentFixture<EventOccurrenceOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventOccurrenceOverviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EventOccurrenceOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
