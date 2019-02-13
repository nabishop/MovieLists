import { Component, OnInit } from '@angular/core';
import { ListService } from 'src/app/shared/list.service';
import { UserComponent } from 'src/app/user/user.component';
import { LoginService } from 'src/app/shared/login.service';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ListItem } from './listitem';
import { MovieModel } from './moviemodel';
import { MovieService } from 'src/app/shared/movie.service';
import { SearchdMovie } from './searchedmovie';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-movies-listitem',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  listsSubscription: Subscription;
  lists: ListItem[];
  curSearch: string;
  ctrl = new FormControl(null, Validators.required);
  starSelected = 0;

  constructor(private listService: ListService, private loginService: LoginService, private toastr: ToastrService, private movieService: MovieService) { }

  ngOnInit() {
    console.log(this.loginService.user);
    this.getListsOfUser();
    this.curSearch = "";
    this.lists = [];
  }

  getListsOfUser() {
    this.listsSubscription = this.listService.getLists(this.loginService.user.id).subscribe(resp => {
      console.log('stat is in getlist ' + resp.status);

      // user is found in db
      if (resp.status.toString() == '200') {
        // error: passwords not the same but right user
        this.lists = resp.body;
        this.lists.sort(this.sortListByDateAdded);

        for (let list of this.lists) {
          this.movieService.getMoviesForList(list.name).subscribe(resp => {
            list.movielist = resp.body;
          });
        }
      }
      else {
        this.toastr.error('Error retriving lists', 'List retrieval failed.');
      }
    });
  }

  deleteList(name: string) {
    console.log(name);
    this.listService.deleteList(this.loginService.user.id, name).subscribe(resp => {
      // reload lists
      this.getListsOfUser();
    });
  }

  sortListByDateAdded(one: ListItem, two: ListItem) {
    if (one.dateAdded > two.dateAdded) return -1;
    else if (one.dateAdded < two.dateAdded) return 1;
    return 0;
  }

  editListName(oldname: string, newname: string) {
    this.listService.renameList(this.loginService.user.id, oldname, newname).subscribe(resp => {
      this.movieService.renameMovieWithList(oldname, newname).subscribe(resp => {
        this.getListsOfUser();
      });
    });
  }

  deleteMovie(id: number) {
    this.movieService.deleteMoviesWithId(id).subscribe(resp => {
      this.getListsOfUser();
    })
  }

  searchOmdb($event, list: ListItem) {
    this.curSearch = $event.target.value;
    console.log(this.curSearch);

    if (this.curSearch.length >= 2) {
      this.movieService.searchMovie(this.curSearch).subscribe(resp => {
        console.log(resp);
        let stringresp = JSON.stringify(resp);
        console.log(stringresp);
        let objresp = JSON.parse(stringresp);
        console.log(objresp);

        list.searchlist = [{
          Title: objresp.Title,
          Plot: objresp.Plot,
          Released: objresp.Released,
          Rating: 0
        }];
      });
    }
  }

  addSearchedMovie(list: ListItem) {
    this.curSearch = "";

    if (list.searchlist.length > 0) {
      let tempMovie = {
        Title: list.searchlist[0].Title,
        Description: list.searchlist[0].Plot,
        ReleaseDate: list.searchlist[0].Released,
        Rating: 0,
        UserID: this.loginService.user.id,
        ListName: list.name
      };

      this.movieService.addMovie(tempMovie).subscribe(resp => {
        this.getListsOfUser();
      });
    }
    else {
      this.toastr.info('Type in a search first!', 'Add new movie incomplete.');
    }
  }

  updateRating(movie: MovieModel) {
    console.log(movie.rating);

    this.movieService.editRating(movie.title, movie.rating).subscribe();
  }
}
