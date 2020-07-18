import { SolvedTask } from './../models/solved-task.model';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';


import { environment } from '@environments/environment';
import { Task } from '@app/models/task.model';
import { User } from '@app/models/user';


@Injectable({
  providedIn: 'root'
})
export class SolvedTaskService {

  constructor(
    private router: Router,
    private http: HttpClient
  ) { }

  // create
  solve(solvedTask: SolvedTask, uid: string, tid: string) {
    const body = {
      solution: solvedTask.solution,
      userId: uid,
      taskId: tid,
    };
    return this.http.post(`${environment.apiUrl}/solvedtasks/solve/`, body);
  }

  // read all
  getAll() {
    return this.http.get<SolvedTask[]>(`${environment.apiUrl}/solvedtasks/`);
  }

  // read by id's
  getById(userId: string, taskId: string) {
    return this.http.get<SolvedTask>(`${environment.apiUrl}/solvedtasks/user/${userId}/task/${taskId}`);
  }
  // read by uid
  getAllByUserId(userId: string) {
    return this.http.get<SolvedTask[]>(`${environment.apiUrl}/solvedtasks/user/${userId}`);
  }
  // read by tid
  getAllByTaskId(taskId: string) {
    return this.http.get<SolvedTask[]>(`${environment.apiUrl}/solvedtasks/user/${taskId}`);
  }

  // update
  update(anySolvedtaskParamsWithIds: any) {
    return this.http.put(`${environment.apiUrl}/solvedtasks/update`, anySolvedtaskParamsWithIds);
  }

  // delete
  delete(uId: string, tId: string) {
    const body = {
      userId: uId,
      taskId: tId
    };
    // return this.http.delete(`${environment.apiUrl}/solvedtasks/delete`, { body });
    return this.http.request('delete', `${environment.apiUrl}/solvedtasks/delete`, { body });
  }
  // update
  rate(uId: string, tId: string, Mark: string) {
    const body = {
      userId: uId,
      taskId: tId,
      mark: Mark
    };
    return this.http.put(`${environment.apiUrl}/solvedtasks/rate`, body);
  }
}
