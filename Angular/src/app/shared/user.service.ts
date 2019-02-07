import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpResponse } from "@angular/common/http";
import { Observable } from 'rxjs';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rootUrl = 'https://socreatemoviebackend.azurewebsites.net/api';

  constructor(private http: HttpClient) { }

  registerUser(user : User) {
    const body: User = {
      UserName: user.UserName,
      Password: user.Password
    }

    return this.http.post(this.rootUrl + '/user', body)
  }
}
