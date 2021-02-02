import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule} from '@angular/common/http/testing';
import { SpotifyService } from './spotify.service';
import { User } from './user';

describe('SpotifyService', () => {
  let service: SpotifyService;
  let user1: User = {
    userName: "DummyUser", password: "Test123!", 
    firstName: "Johnny", lastName: "Test", 
    email: "johnnytest123!@gmail.com", description: "Dummy description", 
    favourites: null, friends: null, id: 1
  };

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