import { Component, OnInit } from '@angular/core';
import { ListService } from 'src/app/shared/list.service';
import { UserComponent } from 'src/app/user/user.component';
import { LoginService } from 'src/app/shared/login.service';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-movies-listitem',
  templateUrl: './list.component.html',
  styles: ['./list.component.css']
})
export class ListComponent implements OnInit {
  listsSubscription: Subscription;
  public lists = [];

  constructor(private listService: ListService, private loginService: LoginService, private toastr: ToastrService) { }

  ngOnInit() {
    console.log(this.loginService.user);
    this.getListsOfUser();
  }

  getListsOfUser() {
    this.listsSubscription = this.listService.getLists(this.loginService.user.id).subscribe(resp => {
      console.log('stat is ' + resp.status);

      // user is found in db
      if (resp.status.toString() == '200') {
        // error: passwords not the same but right user
        this.lists = resp.body;
        console.log(this.lists);
      }
      else {
        this.toastr.error('Error retriving lists', 'List retrieval failed.');
      }
    });
  }

  onSubmit(listname: string){
    console.log('recieving from list '+listname);
  }

}
