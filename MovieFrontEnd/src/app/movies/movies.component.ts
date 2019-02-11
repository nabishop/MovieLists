import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-movies',
    templateUrl: './movies.component.html',
    styles: []
})
export class MoviesComponent implements OnInit {
    playlists = [];

    constructor() { }

    ngOnInit() {
        
    }

}
