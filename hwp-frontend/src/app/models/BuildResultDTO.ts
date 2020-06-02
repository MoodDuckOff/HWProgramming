export class BuildResultDTO {
  input: string;
  code: string;
  output: string;
  isSuccess: boolean;
  constructor(firstCode: string) {
    this.code = firstCode;
  }
}
