import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

import { Song } from '../song';
import { SongService } from '../song.service';

@Component
({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['../app.component.css', './search.component.css']
})

export class SearchComponent implements OnInit 
{
    public query: string;
    public songIn: Array<Song> = new Array<Song>();

    public bannerSongIds: Array<number> = new Array<number>();
    public songSelected: boolean;
    public selectedSong: Song;

    constructor(public songService: SongService, private route: ActivatedRoute, private router: Router)
    {
        route.queryParams.pipe(filter(params => params.query))
        .subscribe(params => 
        {
            this.query = params.query;
        });

        songService.searchOriginalsByLyrics(this.query).subscribe
        (
            (data) => this.songIn = data,
            () => alert("Error Searching")
        );

        router.routeReuseStrategy.shouldReuseRoute = function () 
        {
            return false;
        };
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

    search()
    {
        this.router.navigate(['/search'], { queryParams: { query: this.query } });
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
