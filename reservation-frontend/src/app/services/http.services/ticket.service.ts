import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TicketState } from '../../models/ticketState';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private http: HttpClient) { }

  getTicketState(eventOccurrenceId: number, ticketState: string): Observable<TicketState> {
    return this.http.get<TicketState>(url + 'Ticket/' + eventOccurrenceId + '/' + ticketState);
  }
}
