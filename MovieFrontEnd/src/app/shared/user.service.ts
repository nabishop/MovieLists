import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { Observable } from 'rxjs';
import { UserResponse } from './userResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly baseURI = 'https://socreatemoviebackend.azurewebsites.net/api';
  
  constructor(private fb: FormBuilder, private http: HttpClient) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  // used in register.component.html to compare password and confirm password
  comparePasswords(fb: FormGroup) {
    let confirmPasswordCtrl = fb.get('ConfirmPassword');

    // passwordMismatch is error if not equal
    if (confirmPasswordCtrl.errors == null || 'passwordMismatch' in confirmPasswordCtrl.errors) {
      if (fb.get('Password').value != confirmPasswordCtrl.value) {
        confirmPasswordCtrl.setErrors({ passwordMismatch: true })
      }
      else {
        confirmPasswordCtrl.setErrors(null);
      }
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Password: this.formModel.value.Passwords.Password
    }

    return this.http.post(this.baseURI + '/user', body);
  }

  checkIfInDatabase(){
    console.log("db" + this.baseURI + '/user/search/' + this.formModel.value.UserName);

    return this.http.get<UserResponse>(this.baseURI + '/user/search/' + this.formModel.value.UserName, {observe: 'response'});
  }
}
