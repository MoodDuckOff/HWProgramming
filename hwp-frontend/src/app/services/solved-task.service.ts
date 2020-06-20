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
export class TaskService {

  constructor(
    private router: Router,
    private http: HttpClient
  ) { }

  // create
  solve(solvedTask: SolvedTask) {
    return this.http.post(`${environment.apiUrl}/solvedtasks/solve/`, solvedTask);
  }

  // read all
  getAll() {
    return this.http.get<SolvedTask[]>(`${environment.apiUrl}/solvedtasks/`);
  }

  // read by id's
  getById(user: User, task: Task) {
    return this.http.get<SolvedTask>(`${environment.apiUrl}/solvedtasks/user/${user.id}/${task.id}`);
  }
  // read by uid
  getAllByUserId(user: User) {
    return this.http.get<SolvedTask[]>(`${environment.apiUrl}/solvedtasks/user/${user.id}`);
  }
  // read by tid
  getAllByTaskId(task: Task) {
    return this.http.get<SolvedTask[]>(`${environment.apiUrl}/solvedtasks/user/${task.id}`);
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
