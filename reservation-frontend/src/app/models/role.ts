import { User } from "./user"

export class Role {
    public id!: number
    public name!: string
    public users!: User[]
}