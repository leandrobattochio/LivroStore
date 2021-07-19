import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoggedinGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    // Guard das rotas que precisam estar logado.
    // Apenas verifica se existe a key do token no localstorage.
    // Em um ambiente de produção deve ser feito mais verificações.
    var token = localStorage.getItem("token");

    if (token) {
      return true;
    } else {
      alert("Você não esta autenticado!");
      return false;
    }
  }

}
