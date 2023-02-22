import { Guid } from "guid-typescript"
import { Ticket } from "./ticket"

export class Reservation {
    public id!: number
    public userId!: Guid
    public name!: string
    public email!: string
    public phoneNumber!: string
    public tickets!: Ticket[]
    public eventOccurrenceId!: number
    public paymentCompleted!: boolean
    public isDeleted!: boolean
}