/**
 * Created by Owner on 9/23/2017.
 */
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/filter';
import * as auth0 from 'auth0-js';
import {Observer, Observable} from 'rxjs';

//The AuthService is completed by Nolawe Woldesemayat
@Injectable()
export class AuthService {
  private observer:Observer<string>;
  useremail:Observable<string> = new Observable(obs=>this.observer=obs)
  userProfile: any;

//auth0 methods are is gathered from Auth0 website tutorial.
  auth0 = new auth0.WebAuth({
    clientID: 'qR3TC5eYbTLozacszNnZDqEk0jtWVQxY',
    domain: 'cs572.auth0.com',
    responseType: 'token id_token',
    audience: 'https://cs572.auth0.com/userinfo',
    redirectUri: 'http://localhost:4200/home',
    scope: 'openid profile'
  });

  constructor(public router: Router) {}

  public login(): void {
    this.auth0.authorize();
  }
  getToken(){
    return localStorage.getItem('id_token');
  }
  public handleAuthentication(): void {
    //console.log("auth i0 "+localStorage.getItem('id_token'));
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';
        this.setSession(authResult);
        this.router.navigate(['/home']);
      } else if (err) {
        this.router.navigate(['/home']);
        console.log(err);
      }
    });
  }

  private setSession(authResult): void {
    // Set the time that the access token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    return new Date().getTime() < expiresAt;
  }


  getPro(){
    console.log("json "+JSON.parse(localStorage.getItem('profile')).name)
    return JSON.parse(localStorage.getItem('profile'));
  }
}
