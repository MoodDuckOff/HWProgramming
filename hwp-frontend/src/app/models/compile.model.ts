export class CompileModel {
  code: string;
  input: string;
  output: string;
  isSuccess: boolean;
  constructor(inputcode: string) {
    this.code = inputcode;
  }
}
