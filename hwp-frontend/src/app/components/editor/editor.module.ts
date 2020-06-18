import { CommonModule } from '@angular/common';
import { EditorComponent } from './editor.component';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { MonacoEditorModule } from '@materia-ui/ngx-monaco-editor';
import { CompileService } from '@app/services/compile.service';

@NgModule(
  {
    declarations: [EditorComponent],
    imports: [
      CommonModule,
      FormsModule,
      MonacoEditorModule
    ],
    exports: [EditorComponent],
    providers: [CompileService],
    bootstrap: [],
  })
export class EditorModule { }
