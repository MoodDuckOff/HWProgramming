import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import {TasksRoutingModule} from '@app/components/user/tasks/tasks-routing.module';
import {SolveComponent} from '@app/components/user/tasks/solve/solve.component';



@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TasksRoutingModule,
  ],
  declarations: [
    LayoutComponent,
    ListComponent,
    SolveComponent
  ]
})
export class TasksModule { }
