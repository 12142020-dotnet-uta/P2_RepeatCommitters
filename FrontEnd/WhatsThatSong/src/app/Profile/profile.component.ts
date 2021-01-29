import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Song } from '../song';
import { User } from '../user';
import { FriendList } from '../friendRequest';
import { LoginService } from '../login.service';
import { SongService } from '../song.service';
import { FriendService } from '../friend.service';

@Component
({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['../app.component.css', './profile.component.css']
})

export class ProfileComponent implements OnInit 
{
    public user: User;// = null;
    public homeUser: boolean;// = false;
    public isFriend: boolean = false;

    //Track List Info
    public songIn: Array<Song> = new Array<Song>();
    public bannerSongIds: Array<number> = new Array<number>();
    public selectedSong: Song;// = null;
    public songSelected: boolean;// = false;

    constructor(public loginService: LoginService, public songService: SongService, public friendService: FriendService, private route: ActivatedRoute, private router: Router)
    {
        let id: number;
        route.params.subscribe(params => 
        {
            if(params.id)  id = params['id'];
            else           id = -1;
        });
        
        if(id == -1 || this.loginService.loggedInUser.id == id)  this.homeUser = true;
        else
        {
            this.homeUser = false;
            this.loginService.getUser(id).subscribe
            (
                (data) => 
                {
                    this.user = data;
                    //Then we get friends
                    this.friendService.checkFriends(this.loginService.loggedInUser.id, data.id).subscribe
                    (
                        (data) => 
                        {
                            if(data)    this.isFriend = true;
                            else        this.isFriend = false;
                        },
                        () => 
                        {
                            this.isFriend = false;
                            alert("Error");
                        }
                    );
                    //Then we get favourites
                    //this.loginService.getFavourites(id).subscribe();
                },
                () => this.homeUser = true
            );
        }

        if(this.homeUser)
        {
            this.user = loginService.loggedInUser;

            //Set the favourite songs
            /*
            this.songSelected = false;
            for(let x = 0; x < this.user.favourites.length && x < 5; x++)
                this.songIn.push(this.user.favourites[x]);
            */
        }
    }
  
    ngOnInit(): void 
    {
    }

    
    displaySong(songId: number): void
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

    makeFriend(): void
    {
        if(!this.homeUser)  //We shouldn't get this option otherwise
        {
            this.friendService.requestFriend(this.loginService.loggedInUser.id, this.user.id).subscribe
            (
                () =>
                {
                    this.friendService.acceptFriend(this.user.id, this.loginService.loggedInUser.id).subscribe
                    (
                        () => 
                        {
                            this.isFriend = true;//temp
                            alert("Sent a friend request to " + this.user.userName);
                        },
                        () => alert("Accept Error")
                    );
                },
                () => alert("Request Error")
            );
        }
    }

    removeFriend(): void
    {
        if(!this.homeUser) //We shouldn't get this option otherwise
        {
            this.friendService.deleteFriend(this.loginService.loggedInUser.id, this.user.id).subscribe
            (
                () => 
                {
                    // => NOW SET DELTE, AND THEN SEND DELTED FRIEND REQUEST
                    this.isFriend = false;
                    alert("You are no longer friends with " + this.user.userName);
                },
                () => alert("Failure")
            );
        }
    }
    
    findFriend(u: User): number
    {
        let index = -1;
        for(let x = 0; x < this.loginService.loggedInUser.friends.length; x++)
            if(u.userName == this.loginService.loggedInUser.friends[x].userName)
            {
                index = x;
                break;
            }
    
        return index;
    }

    //Router Methods
    goToChat(): void
    {
        this.router.navigate(['/chat/' + this.user.id + '/' + this.loginService.loggedInUser.id]);
    }

    goToEdit(): void
    {
        this.router.navigate(['/editProfile/' + this.loginService.loggedInUser.id]);
    }

    goToFavourites(): void
    {
        this.router.navigate(['/favourites/' + this.user.id]);
    }

    goToOriginalMusic(): void
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
