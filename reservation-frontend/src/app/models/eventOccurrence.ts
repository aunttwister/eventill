import { MatTableDataSource } from "@angular/material/table"
import { Event } from "./event"
import { Reservation } from "./reservation"
import { Ticket } from "./ticket"

export class EventOccurrence {
    public id!: number
    public startTime!: Date
    public eventId!: number
    public event!: Event
    public tickets!: Ticket[]
    public totalTicketCount!: number
    public availableTicketCount!: number
    public reservedTicketCount!: number
    public soldTicketCount!: number
    public reservations!: Reservation[]
    public isActive!: boolean
    public isDeleted!: boolean
}