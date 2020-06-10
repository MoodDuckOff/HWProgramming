import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './services';
import { User, Role } from './models';


@Component({
  selector: 'app-boot',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent {
  [x: string]: any;
  user: User;

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.user.subscribe(x => this.user = x);
  }

  get isAdmin() {
    return this.user && this.user.role === Role.Admin;
  }

  get isTeacher() {
    return this.user && this.user.role === Role.Teacher;
  }


  logout() {
    this.authenticationService.logout();
  }
}
