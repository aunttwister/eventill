import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticateUserCommand } from '../../request-commands/authenticateUserCommand';

const url = environment.url;

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private options = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

  constructor(private http: HttpClient) { }

  postLogin(authenticateUserCommand: AuthenticateUserCommand): Observable<any> {
    let request = JSON.stringify(authenticateUserCommand);
    return this.http.post(url + 'Authentication/login', request, this.options);
  }
}
