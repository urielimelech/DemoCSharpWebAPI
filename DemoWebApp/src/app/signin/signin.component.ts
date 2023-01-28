import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IUserCredentials } from '../interfaces/IUserCredentials';
import { AuthenticationModule } from '../Modules/authentication/authentication.module';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {

  signInForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  })

  constructor(private router: Router, private authentication: AuthenticationModule) { }

  onLogin() {
    const values = this.signInForm.value
    if (values.email && values.password) {
      const userCredentials: IUserCredentials = { email: values.email, password: values.password }
      this.authentication.login(userCredentials)
      setTimeout(()=> {
        this.router.navigateByUrl('/store')
      },20)
    }
  }

  onRegister() {
    console.log("register")
    const values = this.signInForm.value
    if (values.email && values.password) {
      const userCredentials: IUserCredentials = { email: values.email, password: values.password }
      this.authentication.register(userCredentials)
    }
  }

  onSubmit() {
    console.log("onSubmit")
  }
}
