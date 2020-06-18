import { Component } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';
import { Role } from './models/role.model';


@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent {
  user: User;

  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  get isAdmin() {
    return this.user && this.user.role === Role.Admin;
  }

  get isTeacher() {
    return this.user && this.user.role === Role.Teacher;
  }

  logout() {
    this.accountService.logout();
  }
}
