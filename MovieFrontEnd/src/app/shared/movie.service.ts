import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { Observable } from 'rxjs';
import { UserResponse } from './userResponse';

@Injectable({
    providedIn: 'root'
})
export class MovieService {

    readonly baseURI = 'https://socreatemoviebackend.azurewebsites.net/api';

    constructor(private fb: FormBuilder, private http: HttpClient) { }
}
