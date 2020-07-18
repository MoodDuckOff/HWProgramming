import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './profile.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule(
  {
    declarations: [ProfileComponent],
    imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule
    ],
    exports: [ProfileComponent],
  })
export class ProfileModule { }
