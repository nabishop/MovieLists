import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/shared/login.service';
import { UserResponse } from '../../shared/userResponse';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
    selector: 'login-registration',
    templateUrl: './login.component.html',
    styles: []
})

export class LoginComponent implements OnInit {

    getSubscription: Subscription;
    userId: number;

    constructor(public service: LoginService, private toastr: ToastrService, private router: Router) { }

    ngOnInit() {
        this.service.formModel.reset();
    }

    onSubmit(buttonClicked: string) {
        console.log('button clicked is ' + buttonClicked);
        if (buttonClicked == "login") {
            this.onSubmitLogin();
        }
        else {
            this.onSubmitRegister();
        }
    }
    onSubmitLogin() {
        console.log('login');
        this.getSubscription = this.service.checkIfInDatabase().subscribe(resp => {
            console.log('stat is ' + resp.status);

            // user is found in db
            if (resp.status.toString() == '200') {
                // error: passwords not the same but right user
                if (resp.body.password != this.service.formModel.value.Password) {
                    this.toastr.error('Password not valid.', 'Login failed.');
                }
                else {
                    this.service.user = resp.body;

                    this.userId = resp.body.id;

                    this.toastr.success(this.service.formModel.value.UserName + ' logged in!', 'Login succssful.');
                    this.service.formModel.reset();
                    this.router.navigate(['/movies']);
                }
            }
            // user is not in db
            else {
                this.toastr.error('Username: ' + this.service.formModel.value.UserName + ' is not registered.', 'Login failed.');
            }
        });
    }

    onSubmitRegister() {
        this.router.navigate(['/user/registration']);
    }

    // no memory leaks
    ngOnDestroy() {
        console.log('destroying');
        if (this.getSubscription != null) { this.getSubscription.unsubscribe(); }
        console.log(this.getSubscription);
    }

}
