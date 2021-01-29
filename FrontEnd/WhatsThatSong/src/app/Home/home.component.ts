import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Song } from '../song';
import { LoginService } from '../login.service';
import { SongService } from '../song.service';
import { FriendService } from '../friend.service';

@Component
({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['../app.component.css', './home.component.css']
})

export class HomeComponent implements OnInit 
{
    public homepageSong: Song;
    public bannerSongIds: Array<number> = new Array<number>();
    public query: string;// = "";
    public userquery: string;// = "";
    
    constructor(public loginService: LoginService, public songService: SongService, public friendService: FriendService, private router: Router)
    {
        //Parse Friend Requests
        //As it currently is, will require another subscription for getting user name.
        //We should fix this in db.
        /*
       if(loginService.loggedIn)
       {
            friendService.getAllFriendRequests(loginService.loggedInUser.id).subscribe
            (
                (data) => 
                {
                    for(let x = 0; x < data.length; x++)
                    {
                        if(friendService.isSent(data[x]) && data[x].to.id == loginService.loggedInUser.id)
                        {
                            if(confirm("Would you like to become friends with " + data[x].from.username + "?"))
                            {
                                friendService.setAccepted(data[x]);
                                friendService.editFriendRequest(data[x].id, data[x]).subscribe();

                                //Add Friends
                                friendService.acceptFriend(loginService.loggedInUser.id, data[x].from.id).subscribe
                                (
                                    () => alert("You are now friends"),
                                    () => alert("Accept Error")
                                );
                            }
                            else
                            {
                                friendService.setRejected(data[x]);
                                friendService.editFriendRequest(data[x].id, data[x]).subscribe();
                            }
                        }
                        else if(friendService.isAccepted(data[x]) && data[x].from.id == loginService.loggedInUser.id)
                        {
                            alert(data[x].to.username + " accepted your friend request.");
                            friendService.deleteFriendRequest(data[x].id).subscribe();
                        }
                        else if(friendService.isRejected(data[x]) && data[x].from.id == loginService.loggedInUser.id)
                        {
                            alert(data[x].to.username + " rejected your friend request.");
                            friendService.deleteFriendRequest(data[x].id).subscribe();
                        }
                        else if(friendService.isDeleted(data[x]) && data[x].to.id == loginService.loggedInUser.id)
                        {
                            alert(data[x].from.username + " is no longer your friend.");
                            friendService.deleteFriendRequest(data[x].id).subscribe();
                        }
                    }
                },
                (error) => alert(error)
            );
        }
        */
    }
  
    ngOnInit(): void 
    {
        //this.homepageSong = songService.songs[0];
        //this.displaySong(Math.floor(Math.random() * 12));
    }

    search(): void
    {
        this.router.navigate(['/search'], { queryParams: { query: this.query } });
    }

    userSearch(): void
    {
        this.router.navigate(['/usersearch'], { queryParams: { query: this.userquery } });
    }
    
    displaySong(songId: number): void
    {
        this.songService.getSong(songId).subscribe
        (
            (data) => 
            {
                this.homepageSong = data;
            },
            (error) => alert(error)
        );
    }

    setBannerSongs(songIds: Array<number>): void
    {
        this.bannerSongIds = songIds;
    }

    getNextBannerSong(): void
    {
        const index = this.bannerSongIds.indexOf(this.homepageSong.id);
        if(this.bannerSongIds.length <= index + 1)
            this.displaySong(this.bannerSongIds[0]);
        else
            this.displaySong(this.bannerSongIds[index + 1]);
    }

    getPrevBannerSong(): void
    {
        const index = this.bannerSongIds.indexOf(this.homepageSong.id);
        if(index <= 0)
            this.displaySong(this.bannerSongIds[this.bannerSongIds.length - 1]);
        else
            this.displaySong(this.bannerSongIds[index - 1]);
    }
    
    test(): void
    {
        alert("Testing.");
        this.loginService.getValidUsers().subscribe
        (
            (data) => alert("SUCCESS"),
            () => alert("ERROR")
        );
    }
}
