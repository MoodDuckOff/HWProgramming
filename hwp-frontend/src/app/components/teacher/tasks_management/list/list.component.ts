import {AccountService} from '@app/services/account.service';
import {Component, OnInit} from '@angular/core';
import {first} from 'rxjs/operators';
import {TaskService} from '@app/services/task.service';
import {User} from '@app/models/user';
import {Task} from '@app/models/task.model';


@Component({ templateUrl: './list.component.html' })
export class ListComponent implements OnInit {
  tasks = null;
  users = null;


  constructor(
    private taskService: TaskService,
    private accountService: AccountService
  ) { }

  ngOnInit() {

    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => {
        this.users = users;
      });

    this.taskService.getAll()
      .pipe(first())
      .subscribe(tasks => {
        this.tasks = tasks;
      });

  }

  deleteTask(id: string) {
    const task = this.tasks.find((x: { id: string; }) => x.id === id);
    task.isDeleting = true;
    this.taskService.delete(id)
      .pipe(first())
      .subscribe(() => {
        this.tasks = this.tasks.filter((x: { id: string; }) => x.id !== id);
      });
  }

  findName(authId: string): string{
    if (this.users == null) {
      return '';
    }
    return this.users.find(x => x.id === authId).username;
  }
}

