import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AppComponent', () => 
{
  beforeEach(async(() => 
    {
    TestBed.configureTestingModule(
      {
      imports: [ RouterTestingModule, HttpClientTestingModule, FormsModule, ReactiveFormsModule ],
      declarations: [ AppComponent ],
      }).compileComponents();
    }));

  it('should create the app', () => 
  {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  // it(`should have as title 'WhatsThatSong'`, () => 
  // {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   const app = fixture.componentInstance;
  //   expect(app.title).toEqual('WhatsThatSong');
  // });
});
