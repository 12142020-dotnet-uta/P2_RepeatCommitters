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
    private authConnection: string = "https://accounts.spotify.com"
    private clientId: string = "c81d713ce2b3474d8bfad80c777ae5d5";
    private redirectURL: string = "https://inharmony.azurewebsites.net";
    private redirectURLEncoded: string = "https%3A%2F%2Finharmony.azurewebsites.net";
    private authorizationString: string = "YzgxZDcxM2NlMmIzNDc0ZDhiZmFkODBjNzc3YWU1ZDU6MTRlODI0ZjMxNGI5NDFkYjhmZWEwYjgyZjg0ZWE1MzY=";

    //User Info
    public loggedIn: boolean;
    public access_token: string;

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
        console.log(code);

        const headers = new HttpHeaders().append('Authorization', 'Basic ' + this.authorizationString)
                                            .append('Content-Type', 'application/x-www-form-urlencoded');
                                            
        //const params = new HttpParams().append('grant_type', "authorization_code").append('code', code).append('redirect_uri', this.redirectURL);
        //return this.http.post<any>(this.authConnection + "/api/token", {headers, params});
        const payload = new HttpParams().set('grant_type', "authorization_code").set('code', code).append('redirect_uri', this.redirectURL);
        return this.http.post<any>(this.authConnection + "/api/token", payload, {headers: headers}); 
        //const body = new URLSearchParams();
        //body.set('grant_type', "authorization_code");
        //body.set('code', code);
        //body.set('redirect_uri', this.redirectURL);
        //return this.http.post<any>(this.authConnection + "/api/token", body.toString(), {headers: headers}); 
        /*
        return this.http.post<any>(this.authConnection + "/api/token", 
        {
            params: {
              grant_type : "authorization_code",
              code : code,
              redirect_uri : this.redirectURL
            },
            headers: {
              'Authorization' : "Basic " + this.authorizationString,
              'Content-Type':'application/x-www-form-urlencoded'
            }
          });
          */
        //return this.http.post<any>(this.authConnection + "/api/token?grant_type=authorization_code&code=" + code
        //                                                        + "&redirect_uri=" + this.redirectURL, {headers: headers});
    }
}