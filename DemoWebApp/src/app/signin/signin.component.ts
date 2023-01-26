import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { IStockItem } from '../interfaces/IStockItem';
import { RequestConfigService } from '../services/requestConfig/request-config.service';

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

  constructor(private requestService: RequestConfigService){}

  onLogin(){
    this.registeredUser = true
  }
  login(){
    const values = this.signInForm.value
    if (values.email && values.password)
      this.requestService.login(values.email, values.password)
  }

  onRegister(){
    this.registeredUser = false
  }
  register(){
    const values = this.signInForm.value
    if (values.email && values.password)
      this.requestService.register(values.email, values.password)
  }

  onSubmit() {
    this.registeredUser ? this.login() : this.register()
  }

  /** testing JWT */
  getItems(){
    const items = this.requestService.getAllItems().subscribe((v: IStockItem[]) => console.log(v))
  }


}
