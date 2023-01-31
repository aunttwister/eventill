import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reservation } from '../models/reservation';
import { CreateReservationCommand } from '../request-commands/createReservationCommand';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  constructor(private http: HttpClient) { }

  postReservation(createReservationCommand: CreateReservationCommand): Observable<any> {
    let request = JSON.stringify(createReservationCommand);
    console.log(request)
    return this.http.post<Reservation>(url + 'Reservation/', JSON.stringify(createReservationCommand), this.options);
  }
}
