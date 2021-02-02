import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { FavouritesComponent } from './favourites.component';
import { Song } from '../song';

describe('FavouritesComponent', () => {
  let component: FavouritesComponent;
  let fixture: ComponentFixture<FavouritesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule],
      declarations: [ FavouritesComponent ]
    })
    .compileComponents();
  }));
  beforeEach(() => {
    fixture = TestBed.createComponent(FavouritesComponent);
    component = fixture.componentInstance;
    //component.songIn = [song];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call displaySong()', () => {
    component.displaySong(1);
    expect(component).toBeTruthy();
  });

  // it('should call delete()', () => {
  //   component.delete(0);
  //   expect(component).toBeTruthy();
  // });

  // it('should call deleteFromFavourites()', () => {
  //   let s: Song;
  //   component.deleteFromFavourites(s,0);
  //   expect(component).toBeTruthy();
  // });

  it('should call getNextBannerSong()', () => {
    component.getNextBannerSong();
    expect(component).toBeTruthy();
  });

  it('should call getPrevBannerSong()', () => {
    component.getPrevBannerSong();
    expect(component).toBeTruthy();
  });
});