import { AccountService } from '@app/services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { TaskService } from '@app/services/task.service';
import { AlertService } from '@app/services/alert.service';
import { User } from '@app/models/user';



@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
  form: FormGroup;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  user: User;
  tId: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private alertService: AlertService,
    private accountService: AccountService
  ) {
    this.user = this.accountService.userValue;
  }

  ngOnInit() {
    this.tId = this.route.snapshot.params.id;
    this.isAddMode = !this.tId;

    this.form = this.formBuilder.group({
    title: ['', Validators.required],
    description: ['', Validators.required]
    });

    if (!this.isAddMode) {
      this.taskService.getById(this.tId)
        .pipe(first())
        .subscribe(x => {
          this.f.title.setValue(x.title);
          this.f.description.setValue(x.description);
        });
    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createTask();
    } else {
      this.updateTask();
    }
  }

  private createTask() {
    this.taskService.create(this.form.value, this.user)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Task added successfully', { keepAfterRouteChange: true });
          this.router.navigate(['.', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

  private updateTask() {
    this.taskService.update(this.tId, this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Update successful', { keepAfterRouteChange: true });
          this.router.navigate(['..', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
