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
  let mockRegisterUser;
  let mockLoginUser;

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
    albumUrl: "dummydata.com/album",
    lyrics: "Dummy quick slim thick wit it"
  };
  let friend: FriendList = {
    id: 1, friendId: 1, requestedFriendId: 2, 
    toUsername: "DummyUser", fromUsername: "Moonlight", 
    status: "pending", friendListLink: 1
  };

  beforeEach(() => {
    // mock the login service
    mockLoginService = jasmine.createSpyObj('LoginService', ['login']);
    mockLoginUser = mockLoginService.login.and.returnValue(of(user1));
    // mock the song service
    mockSongService = jasmine.createSpyObj('SongService', ['getTopFavourites']);
    mockSongService.getTopFavourites.and.returnValue(of(song));
    // mock the friend service
    mockFriendService = jasmine.createSpyObj('FriendService', ['checkFriends']);
    mockFriendService.checkFriends.and.returnValue(of(friend));
    // create the test bed to compare against
    TestBed.configureTestingModule({
      declarations: [ ProfileComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule, HttpClientModule],
      providers: [ {provide: LoginService, UseValue: mockLoginService},{provide: SongService, UseValue: mockSongService},{provide:FriendService, UseValue: mockFriendService}, HttpClientTestingModule]
    })
    .compileComponents();
  });

  beforeEach(() => {
    // create component and test fixture
    fixture = TestBed.createComponent(ProfileComponent);
    // get test component from the fixture
    component = fixture.componentInstance;
    // force login service data to contain user1
    component.loginService.login(user1);
    component.loginService.loggedInUser = user1;
    // detect and save changes
    fixture.detectChanges();
  });

  it('should create', () => {
    component.user = user1;
    expect(component).toBeTruthy();
  });

  it('should call ngOnInit() with different data', ()=>{
    component.loginService.loggedInUser.id = -2;
    component.ngOnInit();
    expect(user1.id).toEqual(-2);
  });

  it('should call displaySong()', ()=>{
    component.displaySong(1);
    expect(component).toBeTruthy();
  });

  it('should call makeFriend()', ()=>{
    component.makeFriend();
    expect(component).toBeTruthy();
  });

  it('should call removeFriend()', ()=>{
    component.removeFriend();
    expect(component).toBeTruthy();
  });

  it('should call findFriend()', ()=>{
    component.loginService.loggedInUser.friends = [user2];
    component.findFriend(user2);
    expect(component).toBeTruthy();
  });

  /* Routes require different implementation to properly test */

  // it('should call goToChat()', ()=>{
  //   component.goToChat();
  //   expect(component).toBeTruthy();
  // });

  // it('should call goToEdit()', ()=>{
  //   component.loginService.loggedInUser.id = 1;
  //   component.goToEdit();
  //   expect(component).toBeTruthy();
  // });

  // it('should call goToFavourites()', ()=>{
  //   component.goToFavourites();
  //   expect(component).toBeTruthy();
  // });

  // it('should call goToOriginalMusic()', ()=>{
  //   component.goToOriginalMusic();
  //   expect(component).toBeTruthy();
  // });

  it('should call getNextBannerSong()', ()=>{
    component.getNextBannerSong();
    expect(component).toBeTruthy();
  });

  it('should call getPrevBannerSong()', ()=>{
    component.getPrevBannerSong();
    expect(component).toBeTruthy();
  });
});