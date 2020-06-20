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
  create(task: Task, author: User) {
    return this.http.post(`${environment.apiUrl}/tasks/create/${author.id}`, task);
  }

  // read
  getAll() {
    return this.http.get<Task[]>(`${environment.apiUrl}/tasks/`);
  }

  // read
  getById(taskId: string) {
    return this.http.get<Task>(`${environment.apiUrl}/tasks/${taskId}`);
  }

  // update
  update(taskId: string, anyTaskParams: any) {
    return this.http.put(`${environment.apiUrl}/tasks/${taskId}`, anyTaskParams);
  }

  // delete
  delete(taskId: string) {
    return this.http.delete(`${environment.apiUrl}/tasks/${taskId}`);


  }

}
