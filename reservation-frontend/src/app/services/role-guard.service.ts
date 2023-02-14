import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthGuardService } from './auth-guard.service';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService {

  constructor(
    private auth: AuthGuardService, 
    private router: Router, 
    private jwtHelper: JwtHelperService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    const expectedRole = route.data['role'];
    const token = sessionStorage.getItem('access_token') as string;
    const decoded = this.jwtHelper.decodeToken(token);
    
    if (!this.auth.canActivate() || decoded.role !== expectedRole) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
