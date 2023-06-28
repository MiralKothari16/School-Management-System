import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../auth/authentication.service';

@Injectable()
export class AuthconfigInterceptor implements HttpInterceptor {

  constructor(private authservice: AuthenticationService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler) {
    const authtoken = this.authservice.getToken();
    request = request.clone({
      setHeaders: {
        Authorization: "Bearer " + authtoken
      }
    });
    return next.handle(request);
  }
}
//: Observable<HttpEvent<unknown>>