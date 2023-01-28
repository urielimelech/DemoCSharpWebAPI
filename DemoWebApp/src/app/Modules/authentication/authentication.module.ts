import { EventEmitter, ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestConfigService } from 'src/app/services/requestConfig/request-config.service';
import { IUserCredentials } from 'src/app/interfaces/IUserCredentials';
import { IToken } from 'src/app/interfaces/IToken';

@NgModule({
  declarations: [],
  providers: [RequestConfigService],
  imports: [
    CommonModule
  ]
})
export class AuthenticationModule {
  private endpoint: string
  constructor(private _requestConfigService: RequestConfigService) {
    this.endpoint = '/Auth'
  }
  isAuthHeaderSet: boolean = false

  login(credentials: IUserCredentials) {
    this._requestConfigService.postRequest(`${this.endpoint}/login`, credentials).subscribe((response: IToken) => {
      this._requestConfigService.setAuthorizationHeader(response.token)
      // localStorage.setItem("jwt", response.token)
      // let e: EventEmitter<any> = new EventEmitter()
      // e.emit()
    })
  }

  register(credentials: IUserCredentials) {
    this._requestConfigService.postRequest(`${this.endpoint}/register`, credentials).subscribe()
  }
}
