import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SongService } from './song.service';
import { Song } from './song';

describe('SongService', () => {
  let service: SongService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(SongService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getSong()', () => {
    service.getSong(1);
    expect(service).toBeTruthy();
  });

  it('should call uploadSong()', () => {
    let s: Song;
    service.uploadSong(s);
    expect(service).toBeTruthy();
  });

  it('should call deleteMySong()', () => {
    service.deleteMySong(1);
    expect(service).toBeTruthy();
  });

  it('should call incrementPlays()', () => {
    service.incrementPlays(1);
    expect(service).toBeTruthy();
  });

  it('should call isFavourite()', () => {
    service.isFavourite(1,1);
    expect(service).toBeTruthy();
  });

  it('should call deleteFavourite()', () => {
    service.deleteFavourite(1,1);
    expect(service).toBeTruthy();
  });

  it('should call searchOriginalsByLyrics()', () => {
    service.searchOriginalsByLyrics("ich");
    expect(service).toBeTruthy();
  });

  it('should call songExists()', () => {
    service.songExists("","");
    expect(service).toBeTruthy();
  });
});
