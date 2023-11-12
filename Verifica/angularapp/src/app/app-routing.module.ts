import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./Components/home/home.module').then(
        (m) => m.HomeModule
      ),
  },
  {
    path: 'login',
    loadChildren: () =>
      import('./Components/login/login.module').then((m) => m.LoginModule),
  },
  {
    path: 'forgotpwd',
    loadChildren: () =>
      import('./Components/forgot-pwd/forgot-pwd.module').then(
        (m) => m.ForgotPwdModule
      ),
  },
  {
    path: 'home',
    loadChildren: () =>
      import('./Components/home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'applist',
    loadChildren: () =>
      import('./Components/app-list/app-list.module').then(
        (m) => m.AppListModule
      ),
    canActivate: [AuthGuard]
  },
  {
    path: 'registerapp',
    loadChildren: () =>
      import('./Components/app-registry/app-registry.module').then(
        (m) => m.AppRegistryModule),
      canActivate: [AuthGuard],
  },
  {
    path: 'userlist',
    loadChildren: () =>
      import('./Components/user-list/user-list.module').then((m) => m.UserListModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'registeruser',
    loadChildren: () =>
      import('./Components/user-registry/user-registry.module').then(
        (m) => m.UserRegistryModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'waitauth',
    loadChildren: () =>
      import('./Components/wait-auth/waith-auth.module').then(
        (m) => m.WaitAuthModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'modifyuser/:id',
    loadChildren: () =>
      import('./Components/user-registry/user-registry.module').then(
        (m) => m.UserRegistryModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'modifyapp/:id',
    loadChildren: () =>
      import('./Components/app-registry/app-registry.module').then(
        (m) => m.AppRegistryModule
      ),
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
