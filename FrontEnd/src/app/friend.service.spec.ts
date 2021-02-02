import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule} from '@angular/common/http/testing';
import { FriendService } from './friend.service';

describe('FriendService', () => {
  let service: FriendService;

  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(FriendService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getFriendList()', () => {
    service.getFriendList(1);
    expect(service).toBeTruthy();
  });

  it('should call checkFriends()', () => {
    service.checkFriends(1,2);
    expect(service).toBeTruthy();
  });

  it('should call requestFriend()', () => {
    let fl: any;
    service.requestFriend(fl);
    expect(service).toBeTruthy();
  });

  it('should call deleteFriend()', () => {
    service.deleteFriend(1,2);
    expect(service).toBeTruthy();
  });

  it('should call getAllFriendRequests()', () => {
    service.getAllFriendRequests(1);
    expect(service).toBeTruthy();
  });

  it('should call editFriendRequest()', () => {
    let fl: any;
    service.editFriendRequest(fl);
    expect(service).toBeTruthy();
  });
});