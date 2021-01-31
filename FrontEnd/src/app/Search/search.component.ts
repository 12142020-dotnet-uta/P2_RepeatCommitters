import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

import { Song } from '../song';
import { SongService } from '../song.service';
import { GeniusService } from '../genius.service';

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
    public songSelected: boolean;
    public selectedSong: Song;
    public selectedSongIndex: number; //For the Banner

    constructor(public songService: SongService, public geniusService: GeniusService, private route: ActivatedRoute, private router: Router)
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

        geniusService.search(this.query).subscribe
        (
            (data) =>
            {
                const results = data["response"]["hits"];
                for(let x = 0; x < results.length; x++)
                {
                    const result = results[x]["result"];
                    let s = new Song(result["title"], result["primary_artist"]["name"], "", "", 2021, "", false);
                    this.songIn.push(s);
                }
                console.log("Success");
            },
            () => alert("Error with Genius API")
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

    displaySong(x: number)
    {
        this.selectedSong = this.songIn[x];
        this.songSelected = true;
        this.selectedSongIndex = x;
    }  

    search()
    {
        this.router.navigate(['/search'], { queryParams: { query: this.query } });
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


    testGenius(): void
    {
        this.geniusService.search(this.query).subscribe
        (
            (data) =>
            {
                const results = data["response"]["hits"];
                const result = data[0]["result"];
                let s = new Song(result["title"], result["primary_artist"]["name"], "", "", 2021, "", false);
                this.songIn.push(s);
                alert("Success");
            },
            () => alert("Error with Genius API")
        );
    }
}
