import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/shared/login.service';
import { UserResponse } from '../../shared/userResponse';
import { observable, Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
    selector: 'login-registration',
    templateUrl: './login.component.html',
    styles: []
})

export class LoginComponent implements OnInit {

    getSubscription: Subscription;
    registerClick: Boolean;

    constructor(public service: LoginService, private toastr: ToastrService, private router: Router) { }

    ngOnInit() {
        this.service.formModel.reset();
    }

    onLoginClick() {
        this.registerClick = false;
    }

    onRegisterClick() {
        this.registerClick = true;
    }

    onSubmit() {
        if (!this.registerClick) {
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
                    this.toastr.success(this.service.formModel.value.UserName + ' logged in!', 'Login succssful.');
                    this.service.formModel.reset();
                    this.router.navigate(['/user/registration']);
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
        if (this.getSubscription != null) { this.getSubscription.unsubscribe(); }
    }

}
