import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';



@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,

  ],
  declarations: [
    LayoutComponent,
    ListComponent,


  ]
})
export class UserModule { }
