import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  showRegistration = false;
  constructor() { }

  ngOnInit(): void {
  }

  toggleRegistration() {
    this.showRegistration = !this.showRegistration;
  }

  cancel(event: boolean) {
    this.showRegistration = event;
  }

}
