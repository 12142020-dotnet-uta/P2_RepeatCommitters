import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Song } from '../song';
import { User } from '../user';
import { LoginService } from '../login.service';
import { SongService } from '../song.service';

@Component
({
    selector: 'app-music',
    templateUrl: './music.component.html',
    styleUrls: ['../app.component.css', './music.component.css']
})

export class MusicComponent implements OnInit 
{
    public user: User;
    public homeUser: boolean;// = false;

    public songIn: Array<Song> = new Array<Song>();
    public songSelected: boolean;
    public selectedSong: Song;
    public selectedSongIndex: number;

    constructor(public loginService: LoginService, public songService: SongService, private route: ActivatedRoute, private router: Router)
    {
        let id: number;
        route.params.subscribe(params => 
                {
                    if(params.id)  id = params['id'];
                    else           id = -1;
                });
                
        if(id == loginService.loggedInUser.id || id == -1)
        {
            this.user = loginService.loggedInUser;
            this.homeUser = true;
            
            //Get Songs
            songService.getUserSongs(this.user.id).subscribe
            (
                (data) => this.songIn = data,
                () => alert("Error getting songs")
            );
            
        }
        else
        {
            loginService.getUser(id).subscribe
            (
                (data) => 
                {
                    this.user = data;
                    this.homeUser = false;
                    //this.songIn = data.songs; 

                    //Get Songs
                    songService.getUserSongs(this.user.id).subscribe
                    (
                        (data) => this.songIn = data,
                        () => alert("Error getting songs")
                    );
                },
                (error) => alert(error)
            );
        }
    }
  
    ngOnInit(): void 
    {
        this.songSelected = false;
    }

    displaySong(x: number)
    {
        this.selectedSong = this.songIn[x];
        this.songSelected = true;
        this.selectedSongIndex = x;
    }  

    //Routing Methods
    addSong(): void
    {
        this.router.navigate(['/upload']);
    }
    
    //Banner Methods
    getNextBannerSong(): void
    {
        if(this.songIn.length <= this.selectedSongIndex + 1)
            this.displaySong(0);
        else
            this.displaySong(this.selectedSongIndex + 1);
    }

    getPrevBannerSong(): void
    {
        if(this.selectedSongIndex <= 0)
            this.displaySong(this.songIn.length - 1);
        else
            this.displaySong(this.selectedSongIndex - 1);
    }
}
