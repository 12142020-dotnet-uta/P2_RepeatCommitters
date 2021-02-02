import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { LoginComponent } from './login.component';
import { User } from '../user';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let user1: User = {
    userName: "DummyUser", password: "Test123!", 
    firstName: "Johnny", lastName: "Test", 
    email: "johnnytest123!@gmail.com", description: "Dummy description", 
    favourites: null, friends: null, id: 1
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule],
      declarations: [ LoginComponent ]
    })
    .compileComponents();
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    component.loginService.loggedInUser = user1;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call login()', ()=>{
    component.login(user1);
    expect(component.loginService.loggedInUser).toEqual(user1);
  });
});