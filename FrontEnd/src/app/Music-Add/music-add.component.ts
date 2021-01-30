import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, ValidationErrors, ValidatorFn, AbstractControl} from '@angular/forms';

import { User } from '../user';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-music-add',
  templateUrl: './music-add.component.html',
  styleUrls: ['../app.component.css', './music-add.component.css']
})

export class MusicAddComponent implements OnInit 
{
	public formdata: FormGroup;// = null;

	constructor(public loginService: LoginService, private router: Router) { }
/*
We will need another DB entry + a convertor to song (so we can pass to tracklist)
public id:number;
    public name: string;
    public artist: string;
    public album: string;
    public albumURL: string;
    public year: number;
    public genre: string;
    public youtubeURL: string;
    public lyrics: string;
*/
	ngOnInit(): void 
	{
        this.formdata = new FormGroup
        ({
			username: new FormControl("", Validators.required),
			password: new FormControl("", Validators.required),
			rePassword: new FormControl("", Validators.required)
        });	
	}
	
	register(user: User): void
	{
        const u = new User(user.userName, user.password, "");//, "", "");
        this.loginService.loggedInUser = u;
        this.loginService.loggedIn = true;

        this.loginService.register(u).subscribe
        (
            () => this.router.navigate(['/']),
            () => alert("There was an error!")
        );
    }

    //Quick access properties for the forms
    get username() { return this.formdata.get('username'); }
    get password() { return this.formdata.get('password'); }
    get rePassword() { return this.formdata.get('rePassword'); }
}
