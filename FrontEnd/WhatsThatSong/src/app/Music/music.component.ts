import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

import { Song } from '../song';
import { LoginService } from '../login.service';
import { SongService } from '../song.service';
import { User } from '../user';

@Component
({
    selector: 'app-music',
    templateUrl: './music.component.html',
    styleUrls: ['../app.component.css', './music.component.css']
})

export class MusicComponent implements OnInit 
{
    //public query: string;
    public user: User;

    public songIn: Array<Song> = new Array<Song>();
    public bannerSongIds: Array<number> = new Array<number>();
    public songSelected: boolean;
    public selectedSong: Song;

    constructor(public loginService: LoginService, public songService: SongService, private route: ActivatedRoute, private router: Router)
    {
        let id: number;
        route.params.subscribe(params => 
                {
                    if(params.id)  id = params['id'];
                    else           id = -1;
                });

        loginService.getUser(id).subscribe
        (
            (data) => 
            {
                this.user = data;
                //this.songIn = data.songs;
            },
            (error) => alert(error)
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

    //Routing Methods
    addSong(): void
    {
        this.router.navigate(['/music/' + this.user.id]);
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
