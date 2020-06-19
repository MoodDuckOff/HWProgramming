import { Component } from '@angular/core';
import { User } from '@app/models/user';
import { AccountService } from '@app/services/account.service';



@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
  user: User;

  get myRole() {
    return this.user.role;
  }

  constructor(private accountService: AccountService) {
    this.user = this.accountService.userValue;
  }
}
