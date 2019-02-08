import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { UserResponse } from '../../shared/userResponse';
import { observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit() {
    var status;
    this.service.checkIfInDatabase().subscribe(resp => {
      status = resp.status;

      console.log('stat is ' + status);

      // error in table since returned 'ok' for finding a user
      if (status == '200') {
        this.toastr.error('Username: '+resp.body.name+' is already taken!', 'Registration failed.')
      }
      // success: user not already in db
      else {
        this.service.register().subscribe();
        this.toastr.success('User is confirmed!', 'Registration succssful.');
        this.service.formModel.reset();
      }
    });
  }

}
