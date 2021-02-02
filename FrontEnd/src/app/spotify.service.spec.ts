import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule} from '@angular/common/http/testing';
import { SpotifyService } from './spotify.service';

describe('SpotifyService', () => {
  let service: SpotifyService;

  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(SpotifyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getauthUrl()', () => {
    service.getAuthUrl();
    expect(service).toBeTruthy();
  });

  it('should call authStepOne()', () => {
    service.authStepOne();
    expect(service).toBeTruthy();
  });

  it('should call authStepTwo()', () => {
      let code: string;
    service.authStepTwo(code);
    expect(service).toBeTruthy();
  });

  it('should call searchTracks()', () => {
    let code: string = "rot";
  service.searchTracks(code);
  expect(service).toBeTruthy();
});
});