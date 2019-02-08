import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { UserResponse } from '../../shared/userResponse';
import { observable } from 'rxjs';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService) { }

  ngOnInit() {
  }

  onSubmit() {
    var status;
    this.service.checkIfInDatabase().subscribe(resp => {
      status = resp.status;

      console.log('stat is ' + status);

    // error in table
    if (status == '200') {

    }
    // success
    else {
      this.service.register().subscribe();
    }
    });
  }

}
