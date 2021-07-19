import { Component, OnInit } from '@angular/core';
import { RegistrarUsuarioCommand } from 'src/app/domain/livro';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {


  usuario: string = "";
  senha: string = "";
  email: string = "";
  tipo: number = 2;

  constructor(private service: AuthService) { }

  ngOnInit(): void {
  }

  public cadastrar() {

    var command = new RegistrarUsuarioCommand();
    command.Email = this.email;
    command.Senha = this.senha;
    command.Usuario = this.usuario;
    command.TipoUsuario = this.tipo;

    this.service.cadastrar(command).toPromise()
      .then((r) => {
        console.log("Usuario cadastrado!");
      });
  }

}
