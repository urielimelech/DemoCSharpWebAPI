import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {

  registeredUser = false

  signInForm = new FormGroup({
    email: new FormControl(''),
    password:new FormControl('')
  })

  constructor(
  ){}

  onLogin(){
    this.registeredUser = true
  }
  login(){
    console.log(this.signInForm.value)
    console.log("login")
  }

  onRegister(){
    this.registeredUser = false
  }
  register(){
    console.log(this.signInForm.value)
    console.log("register")
  }

  onSubmit() {
    this.registeredUser ? this.login() : this.register()
  }

}
