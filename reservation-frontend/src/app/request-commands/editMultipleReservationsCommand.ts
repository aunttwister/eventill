import { EditReservationCommand } from "./editReservationCommand";

export class EditMultipleReservationsCommand {
    public reservations!: Array<EditReservationCommand>
}