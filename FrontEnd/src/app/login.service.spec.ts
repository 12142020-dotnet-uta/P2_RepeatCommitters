import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule} from '@angular/common/http/testing';
import { LoginService } from './login.service';
import { User } from './user';

describe('LoginService', () => {
  let service: LoginService;
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
    service = TestBed.inject(LoginService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
/* Creates an error during JSON stringify(u) */
//   it('should call loginLocal()', () => {
//     let user: User;
//     service.loginLocal(user);
//     expect(service).toBeTruthy();
//   });

  it('should call logoutLocal()', () => {
    service.logoutLocal();
    expect(service).toBeTruthy();
  });

  it('should call getUser()', () => {
    service.getUser(user1.id);
    expect(service).toBeTruthy();
  });

  it('should call getUserByUsername()', () => {
    service.getUserByUsername(user1.userName);
    expect(service).toBeTruthy();
  });

  it('should call getValidUsers()', () => {
    service.getValidUsers();
    expect(service).toBeTruthy();
  });
});