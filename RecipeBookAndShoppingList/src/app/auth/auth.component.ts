import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { AuthFireBaseResponseData, AuthService } from './auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html'
})
export class AuthComponent implements OnInit {

  authForm!: FormGroup;

  isLoginMode: boolean = false;
  isLoading: boolean = false;
  error: string = '';
  authObs!: Observable<AuthFireBaseResponseData>

  ngOnInit(): void {
    this.initialForm();
  }

  initialForm() {
    let email = '';
    let password = '';
    this.authForm = new FormGroup({
      'email': new FormControl(email, [Validators.required, Validators.email]),
      'password': new FormControl(password, [Validators.required, Validators.minLength(6)]),
    })
  }

  onSwitchMode() {
    this.isLoginMode = !this.isLoginMode;
  }

  onSubmit() {
    if (!this.authForm.valid)
      return;

    this.error = '';
    const email = this.authForm.value.email;
    const password = this.authForm.value.password;

    this.isLoading = true;

    if (this.isLoginMode) {
      this.authObs = this.authService.LogIn(email, password);
    } else {
      this.authObs = this.authService.signUp(email, password);
    }

    this.authObs.subscribe({
      next: res => { console.log(res); this.isLoading = false; },
      error: err => { console.log(err); this.error = err; this.isLoading = false; }
    });

    this.authForm.reset();
  }

  constructor(private authService:AuthService) { }
}
