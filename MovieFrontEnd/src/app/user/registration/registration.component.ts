import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { UserResponse } from '../../shared/userResponse';
import { observable, Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {
  getSubscription: Subscription;
  postSubscription: Subscription;

  constructor(public service: UserService, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit() {
    this.getSubscription = this.service.checkIfInDatabase().subscribe(resp => {
      console.log('stat is ' + resp.status);

      // error in table since returned 'ok' for finding a user
      if (resp.status.toString() == '200') {
        this.toastr.error('Username: ' + resp.body.name + ' is already taken!', 'Registration failed.');
      }
      // success: user not already in db
      else {
        this.postSubscription = this.service.register().subscribe(resp => {
          this.toastr.success('User is confirmed!', 'Registration succssful.');
          this.service.formModel.reset();
          this.router.navigate(['/user/login']);
        });
      }
    });
  }

  // no memory leaks
  ngOnDestroy() {
    if (this.getSubscription != null) { this.getSubscription.unsubscribe(); }
    if (this.postSubscription != null) { this.postSubscription.unsubscribe(); }
  }

}
