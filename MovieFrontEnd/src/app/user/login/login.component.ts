import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { UserResponse } from '../../shared/userResponse';
import { observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'login-registration',
  templateUrl: './login.component.html',
  styles: []
})

export class LoginComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService) { }

  ngOnInit() {
  }

}
