import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import {SolveComponent} from '@app/components/user/tasks/solve/solve.component';


const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      {
        path: '',
        component: ListComponent
      },
      {
        path: ':tid/solve',
        component: SolveComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasksRoutingModule { }
