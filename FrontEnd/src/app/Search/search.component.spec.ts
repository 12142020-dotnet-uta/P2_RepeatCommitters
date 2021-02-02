import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SearchComponent } from './search.component';
import { Song } from '../song';

describe('SearchComponent', () => {
  let component: SearchComponent;
  let fixture: ComponentFixture<SearchComponent>;
  let song: Song = {
    id: 1, title: "Dummy", artistName: "Dummy", 
    album: "DumDumDumDum", year: 1965, 
    genre: "Pop", urlPath: "dummydata.com/album/Dummy", 
    numberOfPlays: 24, isOriginal: true,
    duration: "15", albumURL: "dummydata.com/album",
    albumUrl: "dummydata.com/album",
    lyrics: "Dummy quick slim thick wit it"
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule],
      declarations: [ SearchComponent ]
    })
    .compileComponents();
    fixture = TestBed.createComponent(SearchComponent);
    component = fixture.componentInstance;
    component.query = "";
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call ngOnInit() with different data', () => {
    component.ngOnInit();
    expect(component).toBeTruthy();
  });

  it('should call displaySong()', () => {
    component.displaySong(1);
    expect(component).toBeTruthy();
  });

  it('should call getNextBannerSong()', () => {
    component.getNextBannerSong();
    expect(component).toBeTruthy();
  });

  it('should call getPrevBannerSong()', () => {
    component.getPrevBannerSong();
    expect(component).toBeTruthy();
  });
});