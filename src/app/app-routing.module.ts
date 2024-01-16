import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminRegisterComponent } from './admin-register/admin-register.component';
import { HomePageComponent } from './home-page/home-page.component';
import { AuthGuard } from './guard/auth.guard';

const routes: Routes = [
  {
    path: 'login',
    component: AdminLoginComponent
  },

  {
    path: 'register',
    component: AdminRegisterComponent
  },
  {
    path: 'home',
    component: HomePageComponent,
    canActivate:[AuthGuard]
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {


  














 }
