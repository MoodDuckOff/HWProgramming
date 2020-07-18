import { CompileModel } from '@app/models/compile.model';
import { AlertService } from '@app/services/alert.service';
import { CompileService } from '@app/services/compile.service';
import { Component} from '@angular/core';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-code-editor',
  templateUrl: './editor.component.html'
})
export class EditorComponent {
  ToggleStdin = false;
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
