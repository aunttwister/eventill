export class EditReservationCommand {
    constructor(
        id: number, 
        name: string, 
        email: string, 
        phoneNumber: string, 
        ticketCount: number, 
        eventOccurrenceId: number, 
        paymentCompleted: boolean, 
        isDeleted: boolean) {
        this.id = id
        this.name = name
        this.email = email
        this.phoneNumber = phoneNumber
        this.ticketCount = ticketCount
        this.eventOccurrenceId = eventOccurrenceId
        this.paymentCompleted = paymentCompleted
        this.isDeleted = isDeleted
    }
    public id!: number
    public name!: string
    public email!: string
    public phoneNumber!: string
    public ticketCount!: number
    public eventOccurrenceId!: number
    public paymentCompleted!: boolean
    public isDeleted!: boolean
}