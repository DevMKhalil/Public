import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, ReplaySubject, Subject, tap, throwError } from 'rxjs';
import { User } from './user.model';

export interface AuthFireBaseResponseData {
  kind: string;
  idToken: string;
  email: string;
  refreshToken: string;
  expiresIn: string;
  localId: string;
  registered?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  user = new BehaviorSubject<any>(null);

  signUp(email:string,password:string) {
    return this.http.post<AuthFireBaseResponseData>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyCyKO6bXzOZjgG2FEJC9qXtVAqCQDYhY7E",
      {
        email: email,
        password: password,
        returnSecureToken: true
      })
      .pipe(
        catchError(this.HandelError) // implicitly Pass The Error Parameter
        //catchError(errorRes => {
        //  return this.HandelError(errorRes);
        //})
        , tap(resData => {
          this.handelAuthentication(resData.email, resData.localId, resData.idToken, +resData.expiresIn);
        })
    );
  }

  LogOut() {
    this.user.next(null);
    this.router.navigate(['./auth']);
  }

  LogIn(email: string, password: string) {
    return this.http.post<AuthFireBaseResponseData>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyCyKO6bXzOZjgG2FEJC9qXtVAqCQDYhY7E",
      {
        email: email,
        password: password,
        returnSecureToken: true
      })
      .pipe(
        catchError(errorRes => {
          return this.HandelError(errorRes);
        }),
        tap(resData => {
          this.handelAuthentication(resData.email, resData.localId, resData.idToken, +resData.expiresIn);
        })
      );
  }

  private handelAuthentication(email: string,userId: string, token: string, expiresIn: number) {
    const tokenExpirationDate: Date = new Date(new Date().getTime() + (expiresIn * 1000));
    const user = new User(email, userId, token, tokenExpirationDate);
    this.user.next(user);
  }

  private HandelError(errorRes: HttpErrorResponse) {
    let errorMessage = 'an UnKnowen Error Occurred!';

    if (!errorRes.error || !errorRes.error.error)
      return throwError(errorMessage);

    switch (errorRes.error.error.message) {
      case 'EMAIL_NOT_FOUND':
        errorMessage = 'There is no user record corresponding to this Email. The user may have been deleted';
        break;
      case 'INVALID_PASSWORD':
        errorMessage = 'The password Or Email is invalid or the user does not have a password';
        break;
      case 'USER_DISABLED':
        errorMessage = 'The user account has been disabled by an administrator';
        break;
      case 'EMAIL_EXISTS':
        errorMessage = 'This E-Mail Is Already Exist';
        break;
      case 'OPERATION_NOT_ALLOWED':
        errorMessage = 'Password sign-in is disabled for this project';
        break;
      case 'TOO_MANY_ATTEMPTS_TRY_LATER':
        errorMessage = 'We have blocked all requests from this device due to unusual activity. Try again later';
        break;
      default:
        errorMessage = errorRes.error.error.message;
        break;
    }

    return throwError(errorMessage);
  }

  constructor(private http:HttpClient,private router:Router) { }
}
