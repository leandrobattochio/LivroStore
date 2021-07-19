import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem("token");

    // HttpInterceptor. Intercepa os requests HTTP enviados pela aplicação para adicionar o Header de Autorização
    // caso tenha o token salvo no localstorage.

    if (token) {
      const authRequest = request.clone(
        { setHeaders: { 'Authorization': `Bearer ${token}` } });

      return next.handle(authRequest);
    } else {
      return next.handle(request);
    }
  }
}
