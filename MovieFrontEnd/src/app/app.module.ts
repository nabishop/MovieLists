import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './shared/user.service';
import { LoginService } from './shared/login.service'
import { LoginComponent } from './user/login/login.component';
import { MoviesComponent } from './movies/movies.component';
import { ListService } from './shared/list.service';
import { MovieService } from './shared/movie.service';
import { ListComponent } from './movies/list_item/list.component';
import { MovieItemComponent } from './movies/movie_item/movieitem.component';
import { MatIconModule, MatListModule, MatExpansionModule, MatExpansionPanel, MatExpansionPanelTitle, MatExpansionPanelHeader, MatAccordion } from '@angular/material';
import { PortalModule } from '@angular/cdk/portal';

export { MatIconModule, MatListModule, MatExpansionModule, MatExpansionPanel, MatExpansionPanelTitle, MatExpansionPanelHeader, MatAccordion } from '@angular/material';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    MoviesComponent,
    ListComponent,
    MovieItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    MatIconModule,
    MatListModule,
    PortalModule,
    MatExpansionModule,
    FormsModule
    ],
  providers: [UserService, LoginService, ListService, MovieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
