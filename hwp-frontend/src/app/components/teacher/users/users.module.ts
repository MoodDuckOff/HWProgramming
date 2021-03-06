import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import { UsersRoutingModule } from './users-routing.module';
import {UnsolvedSolvedComponent} from './unsolved-solved/unsolved-solved.component';
import {WatchComponent} from '@app/components/teacher/users/watch/watch.component';



@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UsersRoutingModule
  ],
  declarations: [
    LayoutComponent,
    ListComponent,
    UnsolvedSolvedComponent,
    WatchComponent
  ]
})
export class UsersModule { }
