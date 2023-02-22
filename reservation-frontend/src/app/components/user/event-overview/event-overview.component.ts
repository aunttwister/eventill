import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StateService } from 'src/app/services/state.service';
import { Event } from '../../../models/event';
import { EventOccurrence } from '../../../models/eventOccurrence';
import { EventService } from '../../../services/http.services/event.service';
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
              private router: Router,
              private stateService: StateService) { }

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
    let eventOccurrence = this.events[0].eventOccurrences.filter(eo => eo.id == eventOccurrenceId)[0];
    this.stateService.assignEventOccurrence(eventOccurrence);
    this.router.navigateByUrl('/new-reservation/' + eventName + '/' + eventOccurrenceId, {skipLocationChange: false})
  }

  getTicketState(eventOccurrence: EventOccurrence){
    return eventOccurrence.tickets.filter(t => t.ticketState == "0").length == 0;
  }

  filterEventOccurrences(eventOccurrences: Array<EventOccurrence>){
    return eventOccurrences.filter(eo => eo.isActive);
  }
  filterEventOccurrencesLength(eventOccurrences: Array<EventOccurrence>){
    return eventOccurrences.filter(eo => eo.isActive).length;
  }
}
