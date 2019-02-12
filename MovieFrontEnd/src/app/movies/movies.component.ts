import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http"
import { ListService } from '../shared/list.service';
import { LoginService } from '../shared/login.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ListComponent } from './list_item/list.component';


@Component({
    selector: 'app-movies',
    templateUrl: './movies.component.html',
    styles: []
})
export class MoviesComponent implements OnInit {
    playlists = [];
    @ViewChild(ListComponent) child: ListComponent;

    constructor(private http: HttpClient, private listService: ListService, private loginService: LoginService, private toastr: ToastrService, private router: Router) { }

    ngOnInit() {

    }

    onAddList(listname: string) {
        console.log('recieving from list ' + listname);
        this.listService.addList(this.loginService.user.id, listname).subscribe(resp => {
            console.log(resp.status);
            this.toastr.success('Success! Added ' + listname + '!', 'Adding new list success.');
            this.child.getListsOfUser();
        },
            err => {
                this.toastr.error('No duplicate names allowed!', 'Adding new list failure.');
            });
    }

}
