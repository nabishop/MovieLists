import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { Observable } from 'rxjs';
import { UserResponse } from './userResponse';

@Injectable({
    providedIn: 'root'
})
export class LoginService {

    constructor(private fb: FormBuilder, private http: HttpClient) { }
    readonly baseURI = 'https://socreatemoviebackend.azurewebsites.net/api';

    formModel = this.fb.group({
        UserName: ['', Validators.required],
        Password: ['', Validators.required],
    });

    login() {
        return this.http.get<UserResponse>(this.baseURI + '/user/search'+this.formModel.value.UserName);
    }

    checkIfInDatabase() {
        console.log("db" + this.baseURI + '/user/search/' + this.formModel.value.UserName);

        return this.http.get<UserResponse>(this.baseURI + '/user/search/' + this.formModel.value.UserName, { observe: 'response' });
    }
}
