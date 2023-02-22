import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateReservationCommand } from '../../../request-commands/createReservationCommand';
import { ReservationService } from '../../../services/http.services/reservation.service';
import { NotificationService } from '../../../services/notification.service';
import { TicketState } from '../../../models/ticketState';
import { EventOccurrence } from '../../../models/eventOccurrence';
import { StateService } from 'src/app/services/state.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-reservation',
  templateUrl: './create-reservation.component.html',
  styleUrls: ['../../../app.component.css', '../../../responsive.css', '../../../form.css', './create-reservation.component.css']
})
export class CreateReservationComponent implements OnInit, OnDestroy {

  eventOccurrence = new EventOccurrence();
  isLoaded = false;
  eventOccurrenceId = 0;
  newReservation = new CreateReservationCommand();
  eventName = '';
  ticketState = new TicketState();
  ticketInfoIsLoaded = false;


  reservationForm = new FormGroup(
    {
      name: new FormControl("", Validators.required),
      email: new FormControl("", [Validators.required, Validators.email]),
      phoneNumber: new FormControl("", Validators.required),
      ticketCount: new FormControl<number>(1,  [Validators.required, Validators.min(1), Validators.max(2)])
    }
  )

  constructor(private reservationService: ReservationService,
              private router: Router,
              private route: ActivatedRoute,
              private notificationService: NotificationService,
              private stateService: StateService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>
      {
        this.eventName = params.get("eventName") as string;
        this.eventOccurrenceId = params.get("eventOccurrenceId") as unknown as number;
        this.newReservation.eventOccurrenceId = this.eventOccurrenceId;
      });
    this.newReservation.ticketCount = 1;
    this.eventOccurrence = this.stateService.retrieveEventOccurrence();
    this.getTicketState();
  }

  ngOnDestroy(): void {
    this.stateService.clearEventOccurrence();
  }

  getTicketState()
  {
    this.ticketState.count = this.eventOccurrence.tickets.filter(t => t.ticketState == "0").length;
    this.ticketState.price = this.eventOccurrence.tickets[0].price;
    this.ticketInfoIsLoaded = true;
  }

  onSubmit(): void {
    this.route.paramMap.subscribe(params =>
      {
        this.newReservation.eventOccurrenceId = params.get("eventOccurrenceId") as unknown as number
      });
    
    this.mapFormToModel()
    console.log(this.newReservation)
    
    this.reservationService.postReservation(this.newReservation).subscribe(
      res => {
        console.log(res);
        this.stateService.clearEventOccurrence();
        this.router.navigate(["confirmation/" + this.eventName]);;
      },
      err => {console.log(err);
        this.notificationService.showError(err.error.error, 'StatusCode: ' + err.status);
      }
    );
  }

  mapFormToModel(){
    this.newReservation.name = this.reservationForm.controls['name'].value as string;
    this.newReservation.email = this.reservationForm.controls['email'].value as string;
    this.newReservation.phoneNumber = this.reservationForm.controls['phoneNumber'].value as string;
    this.newReservation.ticketCount = this.reservationForm.controls['ticketCount'].value as number;
  }

  changeTicketCount(amount: number){
    let value = this.reservationForm.controls['ticketCount'].value as number;
    if (value + amount < 1 || value + amount > 2)
      return;
    this.reservationForm.controls['ticketCount'].setValue(value + amount);
  }

  checkFormControlValidation(control: string){
    switch (control) {
      case 'name':
        return this.reservationForm.controls['name'].valid || this.reservationForm.controls['name'].pristine;
      case 'email':
        return this.reservationForm.controls['email'].valid || this.reservationForm.controls['email'].pristine;
      case 'ticketCount':
        return this.reservationForm.controls['ticketCount'].valid || this.reservationForm.controls['ticketCount'].pristine || this.reservationForm.controls['ticketCount'].value as number > 2;
      case 'ticketCountMax':
        return this.reservationForm.controls['ticketCount'].valid || this.reservationForm.controls['ticketCount'].pristine || this.reservationForm.controls['ticketCount'].value as number < 2;
      default:
        return false;
    }
  }
}
