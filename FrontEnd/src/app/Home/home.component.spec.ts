import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { Song } from '../song';
import { SongService } from '../song.service';
import { LoginService } from '../login.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule],
      declarations: [ HomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
<<<<<<< HEAD

  it('should get the home page song', ()=>{
    component.homepageSong = {
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

    mockHomePageSong = mockService.ngOnInit.and.returnValue(of(component.homepageSong));
    fixture.detectChanges();
    expect(component.homepageSong).toEqual(mockSong);
  });
=======
>>>>>>> 935a191052a6ef80b147735240df9efb88a40a52
});
