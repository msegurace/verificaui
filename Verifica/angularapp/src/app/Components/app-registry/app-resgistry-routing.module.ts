import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRegistryComponent } from './app-registry.component';

const routes: Routes = [
  {
    path: '',
    component: AppRegistryComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AppRegistryRoutingModule { }
