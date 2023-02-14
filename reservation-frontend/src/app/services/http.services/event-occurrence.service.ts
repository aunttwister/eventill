import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EventOccurrence } from '../../models/eventOccurrence';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class EventOccurrenceService {

  constructor(private http: HttpClient) { }

  getEventOccurrence(id: number): Observable<EventOccurrence> {
    return this.http.get<EventOccurrence>(url + 'EventOccurrence/' + id);
  }
}
