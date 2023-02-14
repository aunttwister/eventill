import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reservation } from '../../models/reservation';
import { CreateReservationCommand } from '../../request-commands/createReservationCommand';
import { EditMultipleReservationsCommand } from '../../request-commands/editMultipleReservationsCommand';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  constructor(private http: HttpClient) { }

  postReservation(createReservationCommand: CreateReservationCommand): Observable<any> {
    let request = JSON.stringify(createReservationCommand);
    return this.http.post<Reservation>(url + 'Reservation', request, this.options);
  }

  
  postEventReservations(editReservations: EditMultipleReservationsCommand) {
    let request = JSON.stringify(editReservations);
    console.log(request)
    return this.http.post(url + 'Reservation/edit/multiple', request, this.options)
  }
}
