import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { FriendListComponent } from './friend-list.component';
import { User } from '../user';
import { of } from 'rxjs';

describe('FriendListComponent', () => {
  let component: FriendListComponent;
  let fixture: ComponentFixture<FriendListComponent>;
  let mockLoginService;
  let mockLoginUser;
  let user1: User = {
    userName: "DummyUser", password: "Test123!", 
    firstName: "Johnny", lastName: "Test", 
    email: "johnnytest123!@gmail.com", description: "Dummy description", 
    favourites: null, friends: null, id: 1
  };

  beforeEach(() => {
    mockLoginService = jasmine.createSpyObj('LoginService', ['login']);
    mockLoginUser = mockLoginService.login.and.returnValue(of(user1));
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule],
      declarations: [ FriendListComponent ]
    })
    .compileComponents();
    fixture = TestBed.createComponent(FriendListComponent);
    component = fixture.componentInstance;
    component.loginService.login(user1);
    component.loginService.loggedInUser = user1;
    fixture.detectChanges();
  });
  
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call deleteFriend()', () => {
    component.deleteFriend(user1);
    expect(component).toBeTruthy();
  });

  it('should call findFriend()', () => {
    component.findFriend(user1);
    expect(component).toBeTruthy();
  });

  it('should call displayProfile()', () => {
    component.displayProfile(user1);
    expect(component).toBeTruthy();
  });
});