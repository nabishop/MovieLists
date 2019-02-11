import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { UserResponse } from './userResponse';

@Injectable({
    providedIn: 'root'
})
export class ListService {
    readonly baseURI = 'https://socreatemoviebackend.azurewebsites.net/api';

    constructor(private fb: FormBuilder, private http: HttpClient) { }

    // id is user id
    getLists(id: number) {
        return this.http.get<UserResponse[]>(this.baseURI + '/list/' + id, { observe: 'response' });
    }

    renameList(id: number, oldn: string, newn: string) {
        var change = {
            "oldname": oldn,
            "newname": newn
        };
        this.http.put<UserResponse>(this.baseURI + '/list/' + id, change, { observe: 'response' });
    }

    addList(id: number, name: string) {
        var newList = {
            "name": name,
            "dateAdded": Date.now().toString(),
            "movie_id": null,
            "user_id": id
        }

        this.http.post<UserResponse>(this.baseURI + '/list/' + id, newList, { observe: 'response' });
    }

    deleteList(id: number, name: string) {
        this.http.delete<UserResponse>(this.baseURI + '/list/' + id + '/' + name, { observe: 'response' });
    }
}
