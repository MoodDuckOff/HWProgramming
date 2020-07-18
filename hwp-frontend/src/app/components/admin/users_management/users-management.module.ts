import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { UsersManagementRoutingModule } from './users-management-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './add-edit/add-edit.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UsersManagementRoutingModule
  ],
  declarations: [
    LayoutComponent,
    ListComponent,
    AddEditComponent

  ]
})
export class UsersManagementModule { }
