import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  @ViewChild('f') firstTDFrom!: NgForm;
  public email !: string;
  public password !: string;


  constructor(private authservice: AuthenticationService) { }

  onsubmit() {
    this.authservice.signIn(this.firstTDFrom.value);
    this.email = '';
    this.password = '';
    // this.g////
    // this.authservice.storetoken
    console.log(this.firstTDFrom.value);




  }

}
