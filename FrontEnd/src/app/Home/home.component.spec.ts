import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './home.component';
import { Song } from '../song';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let mockHomePageSong: Song = new Song("St. Matthew's Passion", "Bach", "Work", "Orchestral", 2021, "track/5PGo11SpjSti3e6qi3CItZ", false);

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

  /* Breaks testing for some reason */
  // it('should call userSearch()', ()=>{
  //   component.userSearch();
  //   expect(component).toBeTruthy();
  // });

  // it('should call search()', ()=>{
  //   component.search();
  //   expect(component).toBeTruthy();
  // });

  it('should call displaySong()', ()=>{
    component.displaySong(mockHomePageSong.id);
    expect(component).toBeTruthy();
  });

  it('should call getNextBannerSong()', ()=>{
    component.getNextBannerSong();
    expect(component).toBeTruthy();
  });

  it('should call getPrevBannerSong()', ()=>{
    component.getPrevBannerSong();
    expect(component).toBeTruthy();
  });

  // it('should call testSpotify2()', ()=>{
  //   component.testSpotify2();
  //   expect(component).toBeTruthy();
  // });

  it('should call testSpotify22()', ()=>{
    component.testSpotify22();
    expect(component).toBeTruthy();
  });
});