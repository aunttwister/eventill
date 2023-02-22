import { Reservation } from "../models/reservation"
import { Ticket } from "../models/ticket"

export class EditEventOccurrenceCommand {

    constructor(
        id: number, 
        startTime: Date, 
        eventId: number, 
        isActive: boolean, 
        isDeleted: boolean, 
        tickets: Ticket[], 
        reservations: Reservation[]) {
        this.id = id
        this.startTime = startTime
        this.eventId = eventId
        this.isActive = isActive
        this.isDeleted = isDeleted
        this.tickets = tickets
        this.reservations = reservations
    }

    public id!: number
    public startTime!: Date
    public eventId!: number
    public isActive!: boolean
    public isDeleted!: boolean
    public tickets!: Ticket[] 
    public reservations!: Reservation[]
}