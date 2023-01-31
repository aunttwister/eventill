import { Guid } from "guid-typescript"
import { Ticket } from "./ticket"
import { User } from "./user"

export class Reservation {
    public id!: number
    public userId!: Guid
    public name!: string
    public email!: string
    public phoneNumber!: string
    public tickets!: Ticket[]
    public paymentCompleted!: boolean
}