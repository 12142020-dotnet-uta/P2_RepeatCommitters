import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MusicComponent } from './music.component';
import { of } from 'rxjs';
import { User } from '../user';

describe('MusicComponent', () => {
  let component: MusicComponent;
  let fixture: ComponentFixture<MusicComponent>;
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
      declarations: [ MusicComponent ]
    })
    .compileComponents();
    fixture = TestBed.createComponent(MusicComponent);
    component = fixture.componentInstance;
    component.loginService.login(user1);
    component.loginService.loggedInUser = user1;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});