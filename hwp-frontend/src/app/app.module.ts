import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BuildService } from './services/build.service';

import { MonacoEditorModule } from '@materia-ui/ngx-monaco-editor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MonacoEditorModule,
  ],
  providers: [BuildService],
  bootstrap: [AppComponent],
})
export class AppModule { }
