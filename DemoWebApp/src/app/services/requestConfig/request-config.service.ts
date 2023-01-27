import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestConfigService {
  header
  constructor(private http: HttpClient) {
    this.header = { headers: new HttpHeaders() }
  }

  public postRequest(endpoint: string, body: any): Observable<any> {
    return this.http.post(`https://localhost:7253/api${endpoint}`, body, this.header)
  }

  setAuthorizationHeader(token: string) {
    this.header = { headers: new HttpHeaders().set("authorization", `bearer ${token}`) }
  }

}
