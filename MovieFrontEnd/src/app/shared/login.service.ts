import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { Observable } from 'rxjs';
import { UserResponse } from './userResponse';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(private fb: FormBuilder, private http: HttpClient) { }
    readonly baseURI = 'https://socreatemoviebackend.azurewebsites.net/api';

    formModel = this.fb.group({
        UserName: ['', Validators.required],
        Password: ['', [Validators.required, Validators.minLength(4)]],
    });

    login() {
        var body = {
            UserName: this.formModel.value.UserName,
            Password: this.formModel.value.Password
        }

        return this.http.post(this.baseURI + '/user', body);
    }

    checkIfInDatabase() {
        console.log("db" + this.baseURI + '/user/search/' + this.formModel.value.UserName);

        return this.http.get<UserResponse>(this.baseURI + '/user/search/' + this.formModel.value.UserName, { observe: 'response' });
    }
}
