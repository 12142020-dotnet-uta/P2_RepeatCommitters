import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ProfileEditComponent } from './profile-edit.component';
import { User } from '../user';

describe('ProfileEditComponent', () => {
  let component: ProfileEditComponent;
  let fixture: ComponentFixture<ProfileEditComponent>;

  let user1: User = {
    userName: "DummyUser", password: "Test123!", 
    firstName: "Johnny", lastName: "Test", 
    email: "johnnytest123!@gmail.com", description: "Dummy description", 
    favourites: null, friends: null, id: 1
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule],
      declarations: [ ProfileEditComponent ]
    })
    .compileComponents();
    fixture = TestBed.createComponent(ProfileEditComponent);
    component = fixture.componentInstance;
    component.loginService.login(user1);
    component.loginService.loggedInUser = user1;
    component.user = user1;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});