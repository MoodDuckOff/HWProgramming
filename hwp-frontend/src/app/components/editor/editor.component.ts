import { CompileModel } from './../../models/compile.model';
import { AlertService } from './../../services/alert.service';
import { CompileService } from './../../services/compile.service';
import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-code-editor',
  templateUrl: './editor.component.html'
})
export class EditorComponent {
  ToogleStdin = false;
  loading = false;
  editorOptions = { theme: 'vs-dark', language: 'cpp' };
  compileModel: CompileModel = new CompileModel(
    '#include<iostream>\n\n' +
    'using namespace std;\n\n' +
    'int main()\n{\n' +
    '    cout << "Hello, World!";\n' +
    '    return 0;\n}'
  );

  receivedModel: CompileModel;
  compiled = false;
  constructor(
    private compileService: CompileService,
    private alertService: AlertService
  ) { }


  onCompile(compileModel: CompileModel) {
    this.alertService.clear();
    this.loading = true;

    this.compileService.buildAndRun(compileModel)
      .pipe(first())
      .subscribe(
        (data: CompileModel) => {
          this.receivedModel = data;
          this.compiled = true;
        },
        error => {
          this.alertService.info(error);
        }
      );
    this.loading = false;
  }

}
