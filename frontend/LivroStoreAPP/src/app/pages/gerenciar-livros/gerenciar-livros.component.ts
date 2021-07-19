import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Livro } from 'src/app/domain/livro';
import { LivroService } from 'src/app/services/livros.service';

@Component({
  selector: 'app-gerenciar-livros',
  templateUrl: './gerenciar-livros.component.html',
  styleUrls: ['./gerenciar-livros.component.css']
})
export class GerenciarLivrosComponent implements OnInit {


  modoEditar = false;
  entity: Livro = <any>{};

  constructor(private service: LivroService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    var id = this.route.snapshot.paramMap.get('id') as any;

    if(id != undefined)
    {
      this.modoEditar = true;
      
      this.service.getLivro(id).toPromise()
        .then((r) => {
          console.log(r);
          this.entity = r;
          this.entity.id = id;
        });
    }
  }

  public salvar() {

    this.service.atualizarLivro(this.entity)
      .toPromise()
      .then((r) => {
        alert("Livro atualizado!");
      });
  }

  public cadastrar() {

    this.service.cadastrarLivro(this.entity)
      .toPromise()
      .then((r) => {
        alert("Livro cadastrado!");
      });
  }

}
