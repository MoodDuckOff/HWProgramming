import { AccountService } from '@app/services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { TaskService } from '@app/services/task.service';
import { AlertService } from '@app/services/alert.service';
import { SolvedTaskService } from '@app/services/solved-task.service';
import {User} from '@app/models/user';
import { Task } from '@app/models/task.model';
import {SolvedTask} from '@app/models/solved-task.model';





@Component({ templateUrl: 'unsolved-solved.component.html' })
export class UnsolvedSolvedComponent implements OnInit {
  unsolvedTasks = null; // Task[]
  solvedTasks = null; // SolvedTask[]
  users = null;
  uId: string;
  isUnsolvedMode: boolean;
  user = null;

  constructor(
    private taskService: TaskService,
    private accountService: AccountService,
    private route: ActivatedRoute,
    private alertService: AlertService,
    private router: Router,
    private solvedTaskService: SolvedTaskService,
    public formBuilder: FormBuilder
  ) {
    this.uId = this.route.snapshot.params.id;
    this.isUnsolvedMode = this.router.url.includes('unsolved-tasks');
  }


  ngOnInit() {

    this.accountService.getById(this.uId)
      .pipe(first())
      .subscribe(user => {
        this.user = user;
      });

    this.solvedTaskService.getAllByUserId(this.uId)
      .pipe(first())
      .subscribe(st => {
        this.solvedTasks = st;
      });
    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => {
        this.users = users;
      });
    this.accountService.getAllUnsolvedTaskById(this.uId)
      .pipe(first())
      .subscribe(t => {
        this.unsolvedTasks = t;
      });

  }

  findName(userId: string): string{
    if (this.users == null) {
      return '';
    }
    return this.users.find(x => x.id === userId).username;
  }
  nameOf(user: User): string{
    if (user == null){
      return '';
    }
    return user.username;
  }

}
