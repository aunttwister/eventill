import { Guid } from "guid-typescript"
import { Ticket } from "../models/ticket"

export class EditReservationCommand {
    constructor(
        id: number, 
        name: string, 
        email: string, 
        phoneNumber: string, 
        ticketCount: number, 
        eventOccurrenceId: number, 
        paymentCompleted: boolean, 
        isDeleted: boolean,
        tickets: Ticket[],
        userId: Guid) {
        this.id = id
        this.name = name
        this.email = email
        this.phoneNumber = phoneNumber
        this.ticketCount = ticketCount
        this.eventOccurrenceId = eventOccurrenceId
        this.paymentCompleted = paymentCompleted
        this.isDeleted = isDeleted
        this.tickets = tickets
        this.userId = userId
    }
    public id!: number
    public name!: string
    public email!: string
    public phoneNumber!: string
    public ticketCount!: number
    public eventOccurrenceId!: number
    public paymentCompleted!: boolean
    public isDeleted!: boolean
    public tickets!: Ticket[]
    public userId!: Guid
}