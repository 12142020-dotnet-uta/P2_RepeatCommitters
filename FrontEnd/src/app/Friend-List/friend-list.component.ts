import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from '../user';
import { LoginService } from '../login.service';
import { FriendService } from '../friend.service';


@Component
({
    selector: 'app-friend-list',
    templateUrl: './friend-list.component.html',
    styleUrls: ['../app.component.css', './friend-list.component.css']
})

export class FriendListComponent implements OnInit 
{
    public friends: Array<User> = new Array<User>();

    constructor(private loginService: LoginService, private friendService: FriendService, private router: Router)
    {
        //We will have to change this if we want to see someone else's friends list
        loginService.getFriends(loginService.loggedInUser.id).subscribe
        (
            (data) => {this.friends = data; alert(data.length);},
            () => alert("Error")
        );

        //wtf did I even write this??? maybe I had a point...
        //this.friends = new Array<User>();
        //for(let x = 0; x < this.loginService.loggedInUser.friends.length; x++)
        //    this.friends.push(this.loginService.loggedInUser.friends[x]);
    }
  
    ngOnInit(): void {}
    
    deleteFriend(u: User): void
    {
        /*
        let index = this.findFriend(u);

        if(index > -1)
        {
            this.friends.splice(index, 1);
            this.loginService.loggedInUser.friends = this.friends;
            this.loginService.editUser(this.loginService.loggedInUser.id, this.loginService.loggedInUser).subscribe
            (
                () => {},
                (error) => 
                {
                    this.friends.push(u);
                    this.loginService.loggedInUser.friends = this.friends;
                    alert(error);
                }
            );
        }
        else
            alert("User not found!");
            */
        //CURRENTLY ONLY HOMEUSER HAS ACCESS TO THIS PAGE
        //if(!this.homeUser) //We shouldn't get this option otherwise
        //{
            this.friendService.deleteFriend(this.loginService.loggedInUser.id, u.id).subscribe
            (
               () => 
               {
                   // => NOW SET DELTE, AND THEN SEND DELTED FRIEND REQUEST

                   this.friends.splice(this.findFriend(u), 1);  // Delete friend from component view
                   alert("You are no longer friends with " + u.userName);
               },
               () => alert("Failure")
            );
        //}
    }

    findFriend(u: User): number
    {
        let index = -1;
        for(let x = 0; x < this.friends.length; x++)
            if(u.userName == this.friends[x].userName)
            {
                index = x;
                break;
            }

        return index;
    }

    //Router Methods
    displayProfile(u: User): void
    {
        let index = this.findFriend(u);

        if(index > -1)
            this.router.navigate(['/profile', u.id]);
        else
            alert("User not found!");
    }

    goToChat(u: User): void
    {
        this.router.navigate(['/chat/' + u.id + '/' + this.loginService.loggedInUser.id]);
    }
}