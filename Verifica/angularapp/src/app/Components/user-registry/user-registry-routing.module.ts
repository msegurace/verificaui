import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserRegistryComponent } from './user-registry.component';

const routes: Routes = [
  {
    path: '',
    component: UserRegistryComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRegistryRoutingModule { }
