import { EventOccurrence } from "./eventOccurrence"
import { Reservation } from "./reservation"

export class Ticket {
    public id!: number
    public ticketState!: string
    public price!: number
    public reservationId!: number
    public reservation!: Reservation
    public eventOccurrenceId!: number
    public eventOccurrence!: EventOccurrence
}