import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
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
    //private connection: string = "http://localhost:3000"; //Mocked DB
    //private connection: string = "http://localhost:44250; //Whatever our real backend is
    private connection: string = "/api";
    //private connection: string = "https://p2pipeline.azurewebsites.net";

    constructor(private http: HttpClient){}

    getSong(id: number) : Observable<Song>
    {
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('id', "" + id);
        return this.http.get<Song>(this.connection + "/Song/getSong", {headers, params});
    }

    getAllSongs(): Observable<Song[]>
    {
        return this.http.get<Song[]>(this.connection + "/song");
    }

    editSong(id: number, s: Song): Observable<Song[]>
    {
        return this.http.put<Song[]>(this.connection + "/song/" + id, s);
    }

    uploadSong(s: Song): Observable<Song>
    {
        return this.http.post<Song>(this.connection + "/Song/uploadSong", s);
    }

    getUserSongs(uId: number): Observable<Song[]>
    {
        const headers = new HttpHeaders().append('Content-Type', 'application/json');
        const params = new HttpParams().append('id', "" + uId);
        return this.http.get<Song[]>(this.connection + "/Song/GetAllSongsByACertainUser", {headers, params});
    }

    /*
    incrementPlays(id: number): Observable<Song>
    {

    }
    */
}