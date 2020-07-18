import {Component, OnInit} from '@angular/core';
import {first} from 'rxjs/operators';
import {AccountService} from '@app/services/account.service';
import {TaskService} from '@app/services/task.service';
import {ActivatedRoute, Router} from '@angular/router';
import {AlertService} from '@app/services/alert.service';
import {SolvedTaskService} from '@app/services/solved-task.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';


@Component({templateUrl: './watch.component.html'})
export class WatchComponent implements OnInit {
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
    this.uid = this.route.snapshot.params.uid;
    this.tid = this.route.snapshot.params.tid;

    this.form = this.formBuilder.group(
      {
        mark: ['', Validators.required],
        solution: ['']
      });

    this.solvedTaskService.getById(this.uid, this.tid)
      .pipe(first())
      .subscribe(soltask => {
        this.f.mark.setValue(soltask.mark);
        this.f.solution.setValue(soltask.solution);
      });
  }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.rateTask();
  }

  private rateTask() {
    this.solvedTaskService.rate(this.uid, this.tid, this.form.value.mark)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Solved task rated successfully', {keepAfterRouteChange: false});
          this.router.navigate(['..', {relativeTo: this.route}]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
