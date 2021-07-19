import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginUsuarioCommand } from 'src/app/domain/livro';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  usuario: string = "lelejau";
  senha: string = "123456";

  constructor(private service: AuthService, private router: Router) { }

  ngOnInit(): void {
  }


  public login() {
    var command = new LoginUsuarioCommand();
    command.usuario = this.usuario;
    command.senha = this.senha;


    this.service.login(command).toPromise()
      .then((r) => {

        if (r.data.token) {
          alert("Logado com sucesso!");
          localStorage.setItem("token", r.data.token);
          this.router.navigate(['/lista-livros']);
        }
      })
      .catch((e) => {
        alert("Erro ao efetuar login!");
      });
  }

}
