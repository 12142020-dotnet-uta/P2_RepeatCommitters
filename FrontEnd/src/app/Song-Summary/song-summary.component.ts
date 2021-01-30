import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

import { LoginService } from '../login.service';
import { Song } from '../song';
import { SongService } from '../song.service';

@Component
({
    selector: 'app-song-summary',
    templateUrl: './song-summary.component.html',
    styleUrls: ['../app.component.css', './song-summary.component.css']
})

export class SongSummaryComponent implements OnInit 
{
    @Input() song: Song;
    public lyrics: boolean;
    public isFavourite: boolean = false;

    constructor(public loginService: LoginService, public songService: SongService, public sanitizer: DomSanitizer){}
  
    ngOnInit(): void 
    {
        this.checkFavourite();
        //this.songService.incrementPlays(this.song.id).subscribe(() => {}, () => alert("Error Incrementing Plays"));
    }

    ngOnChanges(): void
    {
        this.lyrics = false;
        this.checkFavourite();
    }

    getURL(): SafeResourceUrl
    {
        return this.sanitizer.bypassSecurityTrustResourceUrl("https://w.soundcloud.com/player/?url=" + this.song.urlPath);
    }

    showLyrics(): void
    {
        if(this.lyrics)  this.lyrics = false;
        else             this.lyrics = true;
    }

    addToFavourites(): void
    {
        if(this.loginService.loggedInUser.favourites.indexOf(this.song) < 0)
        {
            this.loginService.loggedInUser.favourites.push(this.song);
            this.loginService.editUser(this.loginService.loggedInUser.id, this.loginService.loggedInUser).subscribe
            (
                (data) => this.isFavourite = true,
                (error) => 
                {
                    this.loginService.loggedInUser.favourites.pop();
                    alert(error);
                }
            );
        }
    }

    checkFavourite(): void
    {
        /*
        if(this.loginService.loggedIn)
            for(let x = 0; x < this.loginService.loggedInUser.favourites.length; x++)
                if(this.song.id == this.loginService.loggedInUser.favourites[x].id)
                {
                    this.isFavourite = true;
                    return;
                }
*/
        this.isFavourite = false;
    }
}
