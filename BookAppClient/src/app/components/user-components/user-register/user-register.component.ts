import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, ValidatorFn } from '@angular/forms';
import { Router } from '@angular/router';
import { UserRegister } from 'src/app/models/user_models/user-register';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { HttpErrorResponse } from '@angular/common/http';
import { group } from '@angular/animations';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent {
  errorMessage: any;
  userRegisterForm: FormGroup;
  value: string = 'off';



  constructor(private repository: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    this.userRegisterForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      email: new FormControl('', ),
      phoneNumber: new FormControl('0',),
      password: new FormControl('', [Validators.required])
    });

    
  }

  get jsonItems(): { key: string; value: any }[] {
    return Object.entries(this.errorMessage || {}).map(([key, value]) => ({ key, value }));
  }

  validateControl = (controlName: string) => {
    if (this.userRegisterForm.get(controlName).invalid && this.userRegisterForm.get(controlName).touched)
      return true;
    
    return false;
  } 

  hasError = (controlName: string, errorName: string) => {
    if (this.userRegisterForm.get(controlName).hasError(errorName))
      return true;
    
    return false;
  }

  registerUser = (userForm) => {
    if (this.userRegisterForm.valid)
      this.executeUserRegistration(userForm);
  }

  private executeUserRegistration = (userForm) => {
    const user: UserRegister = {
      userName: userForm.userName,
      password: userForm.password,
      email: userForm.email,
      phoneNumber: userForm.phoneNumber
    }
    this.repository.registerUser(user)
    .subscribe(
      {
        next: () => {
          this.router.navigate(['/login'])
        },
        error: (err: HttpErrorResponse) => {
            this.errorMessage = err.error;
        }
      })
  }

  redirectToBookList = () => {
    this.router.navigate(['']);
  }
}
