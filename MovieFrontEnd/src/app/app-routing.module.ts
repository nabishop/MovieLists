import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { MoviesComponent } from './movies/movies.component';
import { ListComponent } from './movies/list_item/list.component';
import { MovieItemComponent } from './movies/movie_item/movieitem.component';

const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ],
  },
  {
    path: 'movies', component: MoviesComponent,
    children: [
      {
        path: 'lists', component: ListComponent,
        children: [{ path: 'movieitem', component: MovieItemComponent }]
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
