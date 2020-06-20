import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './add-edit/add-edit.component';
import { TasksManagementRoutingModule } from './tasks-management-routing.module';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TasksManagementRoutingModule
  ],
  declarations: [
    LayoutComponent,
    ListComponent,
    AddEditComponent

  ]
})
export class TasksManagementModule { }
