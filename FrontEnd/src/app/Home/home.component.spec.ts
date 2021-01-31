import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { Song } from '../song';
import { SongService } from '../song.service';
import { LoginService } from '../login.service';

import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let mockHomePageSong;
  let mockService;
  let mockSong: Song = {
    id: 1, 
    title: "Dummy Title", 
    artistName: "Dummy Artist", 
    album: "", year: 1, 
    genre: "Dummy", 
    urlPath:"", 
    numberOfPlays:0, 
    isOriginal: true, 
    duration: "",
    albumURL: "",
    lyrics: ""
  };

  beforeEach(async() => {
    mockService = jasmine.createSpyObj('SongService', ['ngOnInit']);
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // it('should get the home page song', ()=>{
  //   component.homepageSong = {
  //     id: 1, 
  //     title: "Dummy Title", 
  //     artistName: "Dummy Artist", 
  //     album: "", year: 1, 
  //     genre: "Dummy", 
  //     urlPath:"", 
  //     numberOfPlays:0, 
  //     isOriginal: true, 
  //     duration: "",
  //     albumURL: "",
  //     lyrics: ""
  //   };

  //   mockHomePageSong = mockService.ngOnInit.and.returnValue(of(component.homepageSong));
  //   expect(component.homepageSong).toBe(mockSong);
  // });
});
