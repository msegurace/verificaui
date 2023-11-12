import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ForgotPwdComponent } from './forgot-pwd.component';

const routes: Routes = [
  {
    path: '',
    component: ForgotPwdComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ForgotPwdRoutingModule { }
