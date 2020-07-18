import {AccountService} from '@app/services/account.service';
import {Component, OnInit} from '@angular/core';
import {first} from 'rxjs/operators';
import {TaskService} from '@app/services/task.service';


@Component({templateUrl: './list.component.html'})
export class ListComponent implements OnInit {
  user = null;
  users = null;
  tasks = null;
  uid: string;

  constructor(
    private taskService: TaskService,
    private accountService: AccountService
  ) {
    this.user = this.accountService.userValue;
    this.uid = this.user.id;
  }

  ngOnInit() {

    this.accountService.getAllUnsolvedTaskById(this.uid)
      .pipe(first())
      .subscribe(t => {
        this.tasks = t;
        });

    this.accountService.getAll()
      .pipe(first())
      .subscribe(u => {
        this.users = u;
      });
  }

  findName(authId: string): string {
    if (this.users == null) {
      return '';
    }
    return this.users.find(x => x.id === authId).username;
  }
}

