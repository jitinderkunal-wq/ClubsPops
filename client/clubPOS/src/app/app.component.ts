import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  title = 'clubPOS';

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    this.accountService.loadCurrentUserFromStorage().subscribe(() => {
      // This will load the current user after getting the token from local storage
    });
  }
}