import { Component, OnInit } from '@angular/core';
import { EventService } from 'src/app/services/http.services/event.service';
import { Event } from 'src/app/models/event'
import { AuthenticateUserCommand } from 'src/app/request-commands/authenticateUserCommand';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { NotificationService } from 'src/app/services/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../../../app.component.css', '../../../responsive.css', '../../../form.css', './login.component.css']
})
export class LoginComponent implements OnInit {

  newLogin = new AuthenticateUserCommand();

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService) {

  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.authenticationService.postLogin(this.newLogin).subscribe(
      res => {
        const token = res.access_token;
        sessionStorage.setItem('access_token', token);
        this.router.navigate(['admin']);
        console.log(res);
      },
      err => {console.log(err);
        this.notificationService.showError(err.error.error.message, 'StatusCode: ' + err.status);
      }
    );
  }
}