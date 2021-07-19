import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Livro } from 'src/app/domain/livro';
import { LivroService } from 'src/app/services/livros.service';


@Component({
  selector: 'app-lista-livros',
  templateUrl: './lista-livros.component.html',
  styleUrls: ['./lista-livros.component.css']
})
export class ListaLivrosComponent implements OnInit {

  listaLivros: Livro[] = [];

  dataInicial!: Date;
  dataFinal!: Date;

  palavraChave: string = "";

  public filtrar() {
    this.obterLivros(this.palavraChave, this.dataInicial, this.dataFinal);
  }

  constructor(private router: Router , private service: LivroService) {
    this.obterLivros(this.palavraChave, this.dataInicial, this.dataFinal);
  }

  ngOnInit(): void {
  }

  private obterLivros(palavraChave: string, dataInicial: Date, dataFinal: Date) {
    this.service.getLivros(palavraChave, dataInicial, dataFinal)
      .toPromise()
      .then((r) => {
        if (r != undefined && r.itens != undefined) {
          this.listaLivros = r.itens;
        }else{
          this.listaLivros = [];
        }
      })
  }

  public abrirDetalhes(value : Guid) {
    this.router.navigate(['/livro-detalhes', { id: value }]);
  }

  public excluirLivro(value : Guid) {
    this.service.excluirLivro(value.toString()).toPromise()
      .then((r) => {
        alert("Livro excluido!");
        this.obterLivros(this.palavraChave, this.dataInicial, this.dataFinal);
      });
  }

  public atualizarLivro(value: Guid) {
    this.router.navigate(['/gerenciar-livros', { id: value }]);
  }

}
