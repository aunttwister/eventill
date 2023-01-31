import { EventOccurrence } from "./eventOccurrence"
import { EventType } from "./eventType"
import { Question } from "./question"

export class Event {
    public id!: number
    public name!: string
    public directorName!: string
    public description!: string
    public length!: number
    public eventTypeId!: number
    public eventType!: EventType
    public questions!: Question[]
    public eventOccurrences!: EventOccurrence[]
}