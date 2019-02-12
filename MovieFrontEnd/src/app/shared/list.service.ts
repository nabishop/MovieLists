import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse, HttpClientModule } from "@angular/common/http"
import { UserResponse } from './userResponse';

declare var require: any

@Injectable({
    providedIn: 'root'
})
export class ListService {
    readonly baseURI = 'https://socreatemoviebackend.azurewebsites.net/api';

    constructor(private fb: FormBuilder, private http: HttpClient) { }

    // id is user id
    getLists(id: number) {
        return this.http.get<any[]>(this.baseURI + '/list/' + id, { observe: 'response' });
    }

    renameList(id: number, oldn: string, newn: string) {
        var change = {
            "oldname": oldn,
            "newname": newn
        };
        return this.http.put(this.baseURI + '/list/' + id, change, { observe: 'response' });
    }

    addList(id: number, name: string) {
        var curDate = new Date();
        var dateFormat = require('dateformat');
        dateFormat(curDate, "dddd, mmmm dS, yyyy, h:MM:ss TT");

        var newList = {
            "name": name,
            "dateAdded": curDate,
            "movie_id": 0,
            "user_id": id
        }

        return this.http.post(this.baseURI + '/list', newList, { observe: 'response' });
    }

    deleteList(id: number, name: string) {
        return this.http.delete<UserResponse>(this.baseURI + '/list/' + id + '/' + name, { observe: 'response' });
    }
}
