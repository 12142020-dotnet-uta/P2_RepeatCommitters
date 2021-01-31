import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

import { LoginService } from '../login.service';
import { Song } from '../song';
import { SongService } from '../song.service';
import { SpotifyService } from '../spotify.service';

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

    constructor(public loginService: LoginService, public songService: SongService, public spotifyService: SpotifyService,
                    public sanitizer: DomSanitizer){}
  
    ngOnInit(): void 
    {
        this.checkFavourite();
    }

    ngOnChanges(): void
    {
        this.lyrics = false;
        this.checkFavourite();
        
        if(this.loginService.loggedIn)
            this.songService.incrementPlays(this.song.id).subscribe
            (
                () => this.song.numberOfPlays++, 
                () => alert("Error Incrementing Plays")
            );
    }

    getURL(): SafeResourceUrl
    {
        return this.sanitizer.bypassSecurityTrustResourceUrl("https://w.soundcloud.com/player/?url=" + this.song.urlPath);
    }

    getSpotifyURL(): SafeResourceUrl
    {
        return this.sanitizer.bypassSecurityTrustResourceUrl("https://open.spotify.com/embed/" + this.song.urlPath);
    }

    getGeniusLyrics(): SafeResourceUrl
    {
        return this.sanitizer.bypassSecurityTrustResourceUrl('https://genius.com' + this.song.lyrics);
    }


    showLyrics(): void
    {
        if(this.lyrics)  this.lyrics = false;
        else             this.lyrics = true;
    }

    addToFavourites(): void
    {
        this.songService.addToFavourites(this.song.id, this.loginService.loggedInUser.id).subscribe
        (
            () => alert("Added to Favourites"),
            () => alert("Error adding to favourites")
        );
    }

    checkFavourite(): void
    {
        this.isFavourite = false;
        
        if(this.loginService.loggedIn)
            this.songService.isFavourite(this.song.id, this.loginService.loggedInUser.id).subscribe
            (
                (data) => this.isFavourite = data,
                () => alert("Error checking favourite")
            );
    }
}
