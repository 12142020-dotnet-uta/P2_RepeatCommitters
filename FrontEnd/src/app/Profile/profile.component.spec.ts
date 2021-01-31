import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './profile.component';
import { HttpClientModule } from '@angular/common/http';

import { User } from '../user';
import { FriendList } from '../friendRequest';
import { LoginService } from '../login.service';
import { SongService } from '../song.service';
import { FriendService } from '../friend.service';
import { of } from 'rxjs';
import { Song } from '../song';

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;
  let mockLoginService;
  let mockSongService;
  let mockFriendService;

  let user1: User = {
    userName: "DummyUser", password: "Test123!", 
    firstName: "Johnny", lastName: "Test", 
    email: "johnnytest123!@gmail.com", description: "Dummy description", 
    favourites: null, friends: null, id: 1
  };
  let user2: User = {
    userName: "Moonlight", password: "Test123!", 
    firstName: "Johnny", lastName: "Test", 
    email: "johnnytest123!@zmail.com", description: "Dummy description", 
    favourites: null, friends: null, id: 2
  };
  let song: Song = {
    id: 1, title: "Dummy", artistName: "Dummy", 
    album: "DumDumDumDum", year: 1965, 
    genre: "Pop", urlPath: "dummydata.com/album/Dummy", 
    numberOfPlays: 24, isOriginal: true,
    duration: "15", albumURL: "dummydata.com/album",
    lyrics: "Dummy quick slim thick wit it"
  };
  let friend: FriendList = {
    id: 1, friendId: 1, requestedFriendId: 2, 
    toUsername: "DummyUser", fromUsername: "Moonlight", 
    status: "pending", friendListLink: 1
  };

  beforeEach(async() => {
    mockLoginService = jasmine.createSpyObj('LoginService',['register', 'login', 'getUser']);
    mockLoginService.register.and.returnValue(of(user1));
    mockLoginService.login.and.returnValue(of(user1));
    mockLoginService.getUser.and.returnValue(of(user1));
    mockLoginService.loggedIn = true;
    mockLoginService.loggedInUser = user1;
    mockSongService = jasmine.createSpyObj('SongService', ['getTopFavourites']);
    mockSongService.getTopFavourites.and.returnValue(of(song));
    mockFriendService = jasmine.createSpyObj('FriendService', ['checkFriends']);
    mockFriendService.checkFriends.and.returnValue(of(friend));

    // Compile the test bed component
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule, HttpClientModule],
      declarations: [ ProfileComponent ],
      providers: [
        {provide: LoginService, UseValue: mockLoginService},{provide: SongService, UseValue: mockSongService},{provide:FriendService, UseValue: mockFriendService}, HttpClientTestingModule
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    // create component and test fixture
    fixture = TestBed.createComponent(ProfileComponent);
    // get test component from the fixture
    component = fixture.componentInstance;
    // detect and save changes
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
