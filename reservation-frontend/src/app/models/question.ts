import { Event } from "./event"

export class Question {
    public id!: number
    public content!: string
    public events!: Event[]
}