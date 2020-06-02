import { BuildResultDTO } from './../models/BuildResultDTO';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class BuildService {
  constructor(private http: HttpClient) {}

  postData(buildResult: BuildResultDTO) {
    const body = {
      input: buildResult.input,
      code: buildResult.code,
      output: buildResult.output,
      isSuccess: buildResult.isSuccess,
    };
    return this.http.post('https://localhost:5001/api/build/run', body, {
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET,POST,PUT,DELETE,OPTIONS',
        'Access-Control-Allow-Headers':
          'Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With',
      },
    });
  }
}
