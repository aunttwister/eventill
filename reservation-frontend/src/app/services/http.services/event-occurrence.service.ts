import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EditMultipleEventOccurrencesCommand } from 'src/app/request-commands/editMultipleEventOccurrencesCommand';
import { environment } from 'src/environments/environment';
import { EventOccurrence } from '../../models/eventOccurrence';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class EventOccurrenceService {
  
  private options = { headers: new HttpHeaders()
    .set('Content-Type', 'application/json')};

  constructor(private http: HttpClient) { }

  getEventOccurrence(id: number): Observable<EventOccurrence> {
    return this.http.get<EventOccurrence>(url + 'EventOccurrence/' + id);
  }

  postMultipleEventOccurrences(editEventOccurrences: EditMultipleEventOccurrencesCommand) {
    console.log(this.options)
    let request = JSON.stringify(editEventOccurrences);
    console.log(request)
    return this.http.post(url + 'EventOccurrence/edit/multiple/', request, this.options);
  }
}
