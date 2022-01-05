import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegistration = new EventEmitter();
  model: any = {}

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.signup(this.model).subscribe((response)=> {
      this.cancelReg();
    }, error => {
      console.log(error);
    }
    )
  }

  cancelReg() {
    this.cancelRegistration.emit(false);
  }

}
