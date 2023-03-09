import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Event } from '../../models/event';
import { DatePipe } from '@angular/common';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient,
    private datePipe: DatePipe) { }

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(url + 'Event');
  }

  getEvent(id: number, from?: Date): Observable<Event> {
    let options = { params: new HttpParams() }; 

    if (from !== undefined)
    {
      options.params = new HttpParams({
        fromObject: {
          'from' : this.datePipe.transform(from, 'yyyy-MM-dd HH:mm') as string
        }
      })
    }
    return this.http.get<Event>(url + 'Event/' + id, options);
  }

  getHttpParam(params: any[]) {
    let httpParams = new HttpParams();
    params.forEach(p => 
      {
        httpParams = httpParams.append(p.key, p.value);
      })

    return httpParams;
    }
}
