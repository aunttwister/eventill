import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Event } from '../../../models/event';
import { EventService } from '../../../services/event.service';
@Component({
  selector: 'app-event-overview',
  templateUrl: './event-overview.component.html',
  styleUrls: ['../../../app.component.css', '../../../responsive.css', './event-overview.component.css']
})
export class EventOverviewComponent implements OnInit {

  events = new Array<Event>();
  mainEvent = new Event();
  isLoaded = false;

  constructor(private eventService: EventService,
              private router: Router) { }

  ngOnInit(): void {
    this.getEvents()
  }

  getEvents()
  {
    return this.eventService.getEvents().subscribe(data =>
      {
        this.events = data;
        this.mainEvent = data.filter(e => e.name.toUpperCase() == "MIŠOLOVKA")[0];
        this.isLoaded = true;
      }
    )
  }

  selectEventOccurrence(eventOccurrenceId: number, eventName: string)
  {
    this.router.navigateByUrl('/new-reservation/' + eventName + '/' + eventOccurrenceId, {skipLocationChange: false})
  }
}
