import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Song } from '../song';
import { LoginService } from '../login.service';
import { SongService } from '../song.service';

@Component
({
    selector: 'app-favourites',
    templateUrl: './favourites.component.html',
    styleUrls: ['../app.component.css', './favourites.component.css']
})

export class FavouritesComponent implements OnInit 
{
    public songIn: Array<Song> = new Array<Song>();
    public bannerSongIds: Array<number> = new Array<number>();
    public songSelected: boolean;
    public selectedSong: Song;

    constructor(public loginService: LoginService, public songService: SongService, private route: ActivatedRoute)
    {
        let id: number;
        route.params.subscribe(params => 
                {
                    if(params.id)  id = params['id'];
                    else           id = -1;
                });

        //Just in case
        if(id == -1 && loginService.loggedIn)  id = loginService.loggedInUser.id;

        songService.getFavourites(id).subscribe
        (
            (data) => {this.songIn = data; alert(data.length);},
            () => alert("Error getting Favourites")
        );
    }
  
    ngOnInit(): void 
    {
        this.songSelected = false;
    }

    displaySong(songId: number)
    {
        this.songService.getSong(songId).subscribe
        (
            (data) => 
            {
                this.selectedSong = data;
                this.songSelected = true;
            },
            (error) => alert(error)//error/failure
        );
    }
    
    //Banner Methods
    setBannerSongs(songIds: Array<number>): void
    {
        this.bannerSongIds = songIds;
    }

    getNextBannerSong(): void
    {
        const index = this.bannerSongIds.indexOf(this.selectedSong.id);
        if(this.bannerSongIds.length <= index + 1)
            this.displaySong(this.bannerSongIds[0]);
        else
            this.displaySong(this.bannerSongIds[index + 1]);
    }

    getPrevBannerSong(): void
    {
        const index = this.bannerSongIds.indexOf(this.selectedSong.id);
        if(index <= 0)
            this.displaySong(this.bannerSongIds[this.bannerSongIds.length - 1]);
        else
            this.displaySong(this.bannerSongIds[index - 1]);
    }
}
