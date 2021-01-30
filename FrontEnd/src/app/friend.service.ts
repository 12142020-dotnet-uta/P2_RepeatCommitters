import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs";

import { FriendList } from "./friendRequest";

@Injectable
({
    providedIn: 'root'
})
  
export class FriendService 
{
    //DB Strings
    //private connection: string = "http://localhost:3000"; //Mocked DB
    //private connection: string = "http://localhost:44250; //Whatever our real backend is
    private connection: string = "/api";

    constructor(private http: HttpClient){}

    //HTTP Methods
    getFriendList(id: number): Observable<FriendList[]>
	{
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('id', "" + id);
		return this.http.get<FriendList[]>(this.connection + "/user/GetFriends", {headers, params});
    }

    checkFriends(idA: number, idB: number): Observable<boolean>
    {
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('userId', "" + idA).append('friendId', "" + idB);
        return this.http.get<boolean>(this.connection + "/user/AreWeFriends", {headers, params}); 
    }

    requestFriend(idA: number, idB: number): Observable<any>
    {
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('userId', "" + idA).append('requestedFriendId', "" + idB);
        return this.http.get<any>(this.connection + "/user/RequestFriend", {headers, params}); 
    }

    acceptFriend(idA: number, idB: number): Observable<any>
    {
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('LoggedInId', "" + idA).append('pendingFriendId', "" + idB);
        return this.http.get<any>(this.connection + "/user/AcceptFriend", {headers, params}); 
    }
    
    deleteFriend(idA: number, idB: number): Observable<any>
    {
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('LoggedInUserId', "" + idA).append('friendToDeleteId', "" + idB);
        return this.http.get<any>(this.connection + "/user/DeleteFriend", {headers, params}); 
    }

    //Helper Methods
    setSent(fr: FriendList): void {fr.status = "Sent";}
    setAccepted(fr: FriendList): void {fr.status = "Accepted";}
    setRejected(fr: FriendList): void {fr.status = "Rejected";}
    setDeleted(fr: FriendList): void {fr.status = "Deleted";}
    isSent(fr: FriendList): boolean {return fr.status == "Sent";}
    isAccepted(fr: FriendList): boolean {return fr.status == "Accepted";}
    isRejected(fr: FriendList): boolean {return fr.status == "Rejected";}
    isDeleted(fr: FriendList): boolean {return fr.status == "Deleted";}
}