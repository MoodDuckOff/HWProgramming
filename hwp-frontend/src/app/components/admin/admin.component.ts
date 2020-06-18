import { User } from '@app/models/user';
import { AccountService } from '@app/services/account.service';
import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';



@Component({
  templateUrl: 'admin.component.html'
})
export class AdminComponent implements OnInit {
  loading = false;
  users: User[] = [];

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.loading = true;
    this.accountService.getAll().pipe(first()).subscribe(users => {
      this.loading = false;
      this.users = users;
    });
  }
}
