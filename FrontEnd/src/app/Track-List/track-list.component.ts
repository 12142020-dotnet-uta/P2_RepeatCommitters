import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { Song } from '../song';
import { SongService } from '../song.service';

@Component
({
    selector: 'app-track-list',
    templateUrl: './track-list.component.html',
    styleUrls: ['../app.component.css', './track-list.component.css']
})

export class TrackListComponent implements OnInit 
{
    @Input() search: string;
    @Input() songIn: Array<Song> = null;
    @Output() foundSongId = new EventEmitter<number>();
    @Output() allSongIds = new EventEmitter<Array<number>>();
    public songs: Array<Song>;

    constructor(public songService: SongService){}
  
    ngOnInit(): void 
    {
        //What about new user no favourites??????

        
        if(this.songIn != null && this.songIn.length > 0)
        {
            this.songs = this.songIn;
            this.emitBannerIds();
            return;
        }
/*
        //By default we just display top songs
        if(this.search == null || this.search.length < 1)
        {
            this.songService.getAllSongs().subscribe
            (
                (data) => 
                {
                    this.songs = data;
                    this.emitBannerIds();
                },
                (error) => alert(error)//error/failure
            );
        }
        else
        {
            //Else we search
            //We should move this to Search component and pass in through songIn
            this.songs = new Array<Song>();

            this.songService.getAllSongs().subscribe
            (
                (data) => 
                {
                    for(let x = 0; x < data.length; x++)         
                        if(data[x].lyrics.toLowerCase().includes(this.search.toLowerCase()))         
                            this.songs.push(data[x]);

                    this.emitBannerIds();
                },
                (error) => alert(error)//error/failure
            );
        }
    */
    }

    //This should be Refactored so that we submit the whole song
    displaySong(song: Song): void
    {
        this.foundSongId.emit(song.id);
    }

    emitBannerIds(): void
    {
        //Pass All song Ids to the banner
        let songIds = new Array<number>();
        for(let x = 0; x < this.songs.length; x++)
            songIds.push(this.songs[x].id);
        this.allSongIds.emit(songIds);
    }
}
