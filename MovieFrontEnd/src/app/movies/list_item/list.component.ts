import { Component, OnInit } from '@angular/core';
import { ListService } from 'src/app/shared/list.service';
import { UserComponent } from 'src/app/user/user.component';
import { LoginService } from 'src/app/shared/login.service';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ListItem } from './listitem';

@Component({
  selector: 'app-movies-listitem',
  templateUrl: './list.component.html',
  styles: ['./list.component.css']
})
export class ListComponent implements OnInit {
  listsSubscription: Subscription;
  lists: ListItem[];

  constructor(private listService: ListService, private loginService: LoginService, private toastr: ToastrService) { }

  ngOnInit() {
    console.log(this.loginService.user);
    this.getListsOfUser();
  }

  getListsOfUser() {
    this.listsSubscription = this.listService.getLists(this.loginService.user.id).subscribe(resp => {
      console.log('stat is in getlist ' + resp.status);

      // user is found in db
      if (resp.status.toString() == '200') {
        // error: passwords not the same but right user
        this.lists = resp.body;
        console.log(this.lists);
        this.lists.sort(this.sortListByDateAdded);
        console.log(this.lists);

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
      this.getListsOfUser();
    });
  }
}
