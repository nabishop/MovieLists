import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { Observable } from 'rxjs';
import { UserResponse } from './userResponse';
import { MovieModel } from '../movies/list_item/moviemodel';

@Injectable({
    providedIn: 'root'
})
export class MovieService {

    readonly baseURIDB = 'https://socreatemoviebackend.azurewebsites.net/api';
    readonly baseURIOMDB = 'http://www.omdbapi.com';
    private readonly apiKey = '&apikey=2313d21a2313d21a';

    constructor(private fb: FormBuilder, private http: HttpClient) { }

    searchMovie(movieName: string){
        return this.http.get(this.baseURIOMDB+'/?t='+movieName+this.apiKey,  { observe: 'response' })
    }

    getMoviesForList(listname: string){
        return this.http.get<MovieModel[]>(this.baseURIDB+'/movie/'+listname,  { observe: 'response' })
    }

    renameMovieWithList(oldname: string, newname: string){
        return this.http.put(this.baseURIDB+'/movie/'+oldname+'/'+newname,  { observe: 'response' })
    }

    deleteMoviesWithId(id: number){
        return this.http.delete(this.baseURIDB+'/movie/'+id,  { observe: 'response' })
    }
}
