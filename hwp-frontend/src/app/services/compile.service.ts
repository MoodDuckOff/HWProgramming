import { CompileModel } from './../models/compile.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';




@Injectable({
  providedIn: 'root'
})
export class CompileService {
  constructor(
    private http: HttpClient
  ) { }

  buildAndRun(compileModel: CompileModel) {
    return this.http.post(`${environment.apiUrl}/build`, compileModel);
  }


}
