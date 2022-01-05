import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}
  loggedIn: Boolean = false;
  currentUser$: Observable<User>

  constructor(private accountServic:AccountService) { }

  ngOnInit(): void {
    //this.getCurrentUser();
    //Using Pipe
    this.currentUser$ = this.accountServic.currentUser$;
  }

  getCurrentUser() {
    this.accountServic.currentUser$.subscribe((user) => {
      this.loggedIn = !!user;
    }, error => {
      this.loggedIn = false;
    })
  }

  login() {
    console.log(this.model);
    this.accountServic.login(this.model).subscribe((response) => {
      console.log(response);
    }, (error) => {
      console.log(error);
      this.loggedIn = false;
    });
  }

  logout() {
    console.log("Logging out")
    this.accountServic.logout();
  }

}
