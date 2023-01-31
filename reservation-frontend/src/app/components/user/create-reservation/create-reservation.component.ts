import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateReservationCommand } from '../../../request-commands/createReservationCommand';
import { ReservationService } from '../../../services/reservation.service';
import { NotificationService } from '../../../services/notification.service';
import { TicketService } from '../../../services/ticket.service';
import { TicketState } from '../../../models/ticketState';
import { EventOccurrence } from '../../../models/eventOccurrence';

@Component({
  selector: 'app-create-reservation',
  templateUrl: './create-reservation.component.html',
  styleUrls: ['../../../app.component.css', '../../../responsive.css', './create-reservation.component.css']
})
export class CreateReservationComponent implements OnInit {

  eventOccurrence = new EventOccurrence();
  isLoaded = false;
  eventOccurrenceId = 0;
  newReservation = new CreateReservationCommand();
  eventName = '';
  ticketState = new TicketState();
  ticketInfoIsLoaded = false;

  constructor(private reservationService: ReservationService,
              private route: ActivatedRoute,
              private notificationService: NotificationService,
              private ticketService: TicketService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>
      {
        this.eventName = params.get("eventName") as string;
        this.eventOccurrenceId = params.get("eventOccurrenceId") as unknown as number;
        this.newReservation.eventOccurrenceId = this.eventOccurrenceId;
      });
    this.getTicketState();
  }

  getTicketState()
  {
    return this.ticketService.getTicketState(this.eventOccurrenceId, 'Available').subscribe(data =>
        {
          this.ticketState = data;
          this.ticketInfoIsLoaded = true;
        }
      );
  }

  onSubmit(): void {
    this.route.paramMap.subscribe(params =>
      {
        this.newReservation.eventOccurrenceId = params.get("eventOccurrenceId") as unknown as number
      });

      console.log(this.newReservation)
    this.reservationService.postReservation(this.newReservation).subscribe(
      res => {console.log(res)},
      err => {console.log(err)
        this.notificationService.showError(err.error.error, 'StatusCode: ' + err.status)}
    );
  }
}
