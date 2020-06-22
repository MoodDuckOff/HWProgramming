import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import {UnsolvedSolvedComponent} from './unsolved-solved/unsolved-solved.component';
import {WatchComponent} from '@app/components/teacher/users/watch/watch.component';



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
      },
      {
        path: ':uid/task/:tid/watch',
        component: WatchComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
