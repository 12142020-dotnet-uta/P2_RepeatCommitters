import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('AppComponent', () => 
{
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async() => 
    {
    TestBed.configureTestingModule(
      {
      imports: 
      [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      declarations: 
      [
        AppComponent
      ],
      }).compileComponents();
    });

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

  // it('should render title', () => 
  // {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   fixture.detectChanges();
  //   const compiled = fixture.nativeElement;
  //   expect(compiled.querySelector('.content span').textContent).toContain('WhatsThatSong app is running!');
  // });
});
