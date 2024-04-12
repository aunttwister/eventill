import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
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

  event = new Event();
  mainEvent = new Event();
  isLoaded = false;

  constructor(private eventService: EventService,
              private router: Router,
              private notificationService: NotificationService,
              private stateService: StateService) { }

  ngOnInit(): void {
    let dateNow = new Date();
    let eventId = 3;
    this.getEvent(eventId, dateNow)
  }

  getEvent(id: number, from: Date)
  {
    return this.eventService.getEvent(id, from).subscribe(data =>
      {
        this.mainEvent = data;
        this.isLoaded = true;
      },
      err => {
        this.notificationService.showError(err.error.error, 'StatusCode: ' + err.status);
      }
    )
  }

  selectEventOccurrence(eventOccurrenceId: number, eventName: string)
  {
    let eventOccurrence = this.mainEvent.eventOccurrences.filter(eo => eo.id == eventOccurrenceId)[0];
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
    console.log(eventOccurrences)
    return eventOccurrences.filter(eo => eo.isActive).length;
  }
}
