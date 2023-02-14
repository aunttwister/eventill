import { Injectable } from '@angular/core';
import { EventOccurrence } from '../models/eventOccurrence';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  eventOccurrence = new EventOccurrence();

  constructor() { }

  assignEventOccurrence(eventOccurrence: EventOccurrence){
    localStorage.setItem("eventOccurrence", JSON.stringify(eventOccurrence));
  }

  retrieveEventOccurrence(){
    let json = localStorage.getItem("eventOccurrence") as string;
    return JSON.parse(json as string) as EventOccurrence;
  }

  clearEventOccurrence(){
    localStorage.removeItem("eventOccurrence");
  }
}
