import { User } from "./user";

export class FriendList
{
    public id: number;
    public friendId: number;
    public requestedFriendId: number;
    public status: string;

    constructor(f: number, t:number)
    {
        this.friendId = f;
        this.requestedFriendId = t;
        this.status = "Sent";
    }
}