import { Guid } from "guid-typescript";
import { Reservation } from "./reservation";
import { Role } from "./role";

export class User {
    public id!: Guid
    public name!: string
    public email!: string
    public phoneNumber!: string
    public roleId!: number
    public role!: Role
    public reservations!: Reservation[]
}