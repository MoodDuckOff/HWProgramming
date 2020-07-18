import {Component, OnInit} from '@angular/core';
import {first} from 'rxjs/operators';
import {AccountService} from '@app/services/account.service';
import {TaskService} from '@app/services/task.service';
import {ActivatedRoute, Router} from '@angular/router';
import {AlertService} from '@app/services/alert.service';
import {SolvedTaskService} from '@app/services/solved-task.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {SolvedTask} from '@app/models/solved-task.model';


@Component({templateUrl: './solve.component.html'})
export class SolveComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  uid: string;
  tid: string;

  constructor(
    private taskService: TaskService,
    private accountService: AccountService,
    private route: ActivatedRoute,
    private alertService: AlertService,
    private router: Router,
    private solvedTaskService: SolvedTaskService,
    public formBuilder: FormBuilder
  ) {
  }

  get f() {
    return this.form.controls;
  }

  ngOnInit() {
    this.tid = this.route.snapshot.params.tid;
    this.uid = this.accountService.userValue.id;


    this.form = this.formBuilder.group(
      {
        title: [''],
        description: [''],
        solution: ['', Validators.required]
      });

    this.taskService.getById(this.tid)
      .pipe(first())
      .subscribe(x => {
        this.f.title.setValue(x.title);
        this.f.description.setValue(x.description);
      });
  }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.solve();
  }

  private solve() {

    this.solvedTaskService.solve(this.form.value, this.uid, this.tid)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Task published successfully', {keepAfterRouteChange: false});
          this.router.navigate(['..', {relativeTo: this.route}]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
