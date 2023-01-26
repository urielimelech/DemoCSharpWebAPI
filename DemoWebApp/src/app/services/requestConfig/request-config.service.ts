import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { IToken } from 'src/app/interfaces/IToken';
import { IStockItem } from 'src/app/interfaces/IStockItem';


@Injectable({
  providedIn: 'root'
})
export class RequestConfigService {

  headers: HttpHeaders
  token: string
  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders()
    this.token = ''
  }

  private postRequest(endpoint: string, body: any) : Observable<any>{
    console.log(this.headers)
    return this.http.post(`https://localhost:7253/api${endpoint}`, body,  {headers: this.headers})
  }

  private setJsonWebToken(token: string){
    console.log(token)
    this.headers = new HttpHeaders().set("authorization", `bearer ${token}`).set('Access-Control-Allow-Origin', 'https://localhost:4200')
  }

  public login(userEmail: string, password: string){
    this.postRequest('/Auth/login',{email: userEmail, password}).subscribe((response: IToken) => this.setJsonWebToken(response.token))
  }

  public register(userEmail: string, password: string){
    this.postRequest('/Auth/register',{email: userEmail, password}).subscribe()
  }

  public getAllItems() : Observable<IStockItem[]> {
    return this.postRequest('/Store/getItems',null)
  }

}
