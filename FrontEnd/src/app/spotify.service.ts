import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable
({
    providedIn: 'root'
})
  
export class SpotifyService 
{    
    //Connection Constants
    private connection: string = "https://api.spotify.com/v1";
    private authConnection: string = "https://accounts.spotify.com";
    private clientId: string = "c81d713ce2b3474d8bfad80c777ae5d5";
    private redirectURL: string = "https://inharmony.azurewebsites.net";
    private redirectURLEncoded: string = "https%3A%2F%2Finharmony.azurewebsites.net";
    private authorizationString: string = "YzgxZDcxM2NlMmIzNDc0ZDhiZmFkODBjNzc3YWU1ZDU6MTRlODI0ZjMxNGI5NDFkYjhmZWEwYjgyZjg0ZWE1MzY=";

    //User Info
    public loggedIn: boolean;
    public access_token: string;
    //public access_token: string = "BQASLGfsjOyVY1CJ4-JMT844K2m_HYic3cqP5Vxe1f6M8lSYMRu8Ti_IdfJIUG583IlNjdmLWpKRk_YQJ_ZqrrCSi9U9x6-dOL5Rbcpgg0hmHmUBkOd4xFNOsrv5-ifkDvoNYuAXPfY5F1GMxKV0_w7T6b1VLS6WLw";

    constructor(private http: HttpClient)
    {
        this.loggedIn = false;
    }

    getAuthUrl(): string
    {
        return this.authConnection + "/authorize?client_id=" + this.clientId + "&response_type=code&redirect_uri=" + this.redirectURL;  
    }

    authStepOne(): Observable<any>
    {
        console.log(this.authConnection + "/authorize?client_id=" + this.clientId + "&response_type=code&redirect_uri=" + this.redirectURL);
        return this.http.get<any>(this.authConnection + "/authorize?client_id=" + this.clientId + 
                                                            "&response_type=code&redirect_uri=" + this.redirectURLEncoded);
    }

    authStepTwo(code: string): Observable<any>
    {
        const headers = new HttpHeaders().append('Authorization', 'Basic ' + this.authorizationString)
                                            .append('Content-Type', 'application/x-www-form-urlencoded');
        const payload = new HttpParams().set('grant_type', "authorization_code").set('code', code).append('redirect_uri', this.redirectURL);
        return this.http.post<any>(this.authConnection + "/api/token", payload, {headers: headers}); 
    }

    
    searchTracks(query: string): Observable<any>
    {
        const headers = new HttpHeaders().append('Authorization', 'Bearer ' + this.access_token);
        return this.http.get<any>(this.connection + "/search?q=" + query + "&type=track&limit=10", {headers: headers}); 
    }
}