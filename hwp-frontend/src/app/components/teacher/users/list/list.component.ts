import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AccountService } from '@app/services/account.service';



@Component({ templateUrl: './list.component.html' })
export class ListComponent implements OnInit {
  usersLowRole = null;


  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.getAll()
      .pipe(first())
      .subscribe(users => {
        this.usersLowRole = users.filter(u => u.role === 'User');
      });
  }

}
