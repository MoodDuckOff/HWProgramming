import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import {UnsolvedSolvedComponent} from './unsolved-solved/unsolved-solved.component';



const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      {
        path: '',
        component: ListComponent
      },
      {
        path: ':id/unsolved-tasks',
        component: UnsolvedSolvedComponent
      },
      {
        path: ':id/solved-tasks',
        component: UnsolvedSolvedComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
