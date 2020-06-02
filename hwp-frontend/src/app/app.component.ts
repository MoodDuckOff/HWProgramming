import { BuildService } from './services/build.service';
import { Component } from '@angular/core';
import { BuildResultDTO } from './models/BuildResultDTO';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [BuildService],
})
export class AppComponent {
  editorOptions = { theme: 'vs-dark', language: 'cpp' };
  editorCode =
    '#include<iostream>\n\n' +
    'using namespace std;\n\n' +
    'int main()\n{\n' +
    '    cout << "Hello, World!";\n' +
    '    return 0;\n}';
  title = 'hwp-frontend';
  buildResult: BuildResultDTO = new BuildResultDTO(this.editorCode);

  receivedResult: BuildResultDTO;
  done = false;

  constructor(private buildService: BuildService) {}
  submit(buildResult: BuildResultDTO) {
    this.buildService.postData(buildResult).subscribe(
      (data: BuildResultDTO) => {
        this.receivedResult = data;
        this.done = true;
        console.log(this.receivedResult.output);
      },
      (error) => console.log(error)
    );
  }
}
