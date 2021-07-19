import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginUsuarioCommand, RegistrarUsuarioCommand } from '../domain/livro';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http : HttpClient) { }


  public cadastrar(command : RegistrarUsuarioCommand ) : Observable<any> {
    return this.http.post("https://localhost:5001/api/v1/auth/registrar", command);
  }


  public login(command: LoginUsuarioCommand) : Observable<any> {
    return this.http.post("https://localhost:5001/api/v1/auth/login", command);
  }
}