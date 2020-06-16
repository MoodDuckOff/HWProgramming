import { Component } from '@angular/core';

@Component({
  selector: 'app-code-editor',
  templateUrl: './editor.component.html'
})
export class EditorComponent {
  editorOptions = { theme: 'vs-dark', language: 'cpp' };
  code =
    '#include<iostream>\n\n' +
    'using namespace std;\n\n' +
    'int main()\n{\n' +
    '    cout << "Hello, World!";\n' +
    '    return 0;\n}';
}
