import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WaitAuthComponent } from './wait-auth.component';

const routes: Routes = [
  {
    path: '',
    component: WaitAuthComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WaitAuthRoutingModule { }
