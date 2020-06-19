import { Component, OnInit } from '@angular/core';
import { AccountService } from '@app/services/account.service';
import { User } from '@app/models/user';
import { Validator, AbstractControl, NG_VALIDATORS, NgControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Directive, Input } from '@angular/core';
import { first } from 'rxjs/operators';
import { AlertService } from '@app/services/alert.service';


@Component({
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {
  user: User;
  form: FormGroup;
  id: string;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private alertService: AlertService
  ) {
    this.user = this.accountService.userValue;
  }

  ngOnInit() {
    this.id = this.user.id;

    const passwordValidators = [Validators.minLength(6)];
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      role: ['', Validators.required],
      password: ['', passwordValidators]
    });


    this.accountService.getById(this.id)
      .pipe(first())
      .subscribe(x => {
        this.f.firstName.setValue(x.firstName);
        this.f.lastName.setValue(x.lastName);
        this.f.username.setValue(x.username);
        this.f.role.setValue(x.role);
      });

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
    this.updateUser();
  }

  private updateUser() {
    this.accountService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Update successful', { keepAfterRouteChange: false });
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
    this.loading = false;
  }
}



