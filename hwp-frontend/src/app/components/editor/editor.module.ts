import { CommonModule } from '@angular/common';
import { EditorComponent } from './editor.component';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { MonacoEditorModule } from '@materia-ui/ngx-monaco-editor';

@NgModule(
  {
    declarations: [EditorComponent],
    imports: [
      CommonModule,
      FormsModule,
      MonacoEditorModule
    ],
    exports: [EditorComponent],
    providers: [],
    bootstrap: [],
  })
export class EditorModule { }
