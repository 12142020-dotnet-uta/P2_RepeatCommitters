import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { Song } from "./song";

@Injectable
({
    providedIn: 'root'
})
  
export class SongService 
{    
    //DB Strings
    private connection: string = "http://localhost:3000"; //Mocked DB
    //private connection: string = "http://localhost:44250; //Whatever our real backend is

    constructor(private http: HttpClient){}

    getSong(id: number) : Observable<Song>
    {
        return this.http.get<Song>(this.connection + "/song/" + id);
    }

    getAllSongs(): Observable<Song[]>
    {
        return this.http.get<Song[]>(this.connection + "/song");
    }

    editSong(id: number, s: Song): Observable<Song[]>
    {
        return this.http.put<Song[]>(this.connection + "/song/" + id, s);
    }
}