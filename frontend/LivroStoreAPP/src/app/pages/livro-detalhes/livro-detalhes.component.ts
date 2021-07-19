import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Livro } from 'src/app/domain/livro';
import { LivroService } from 'src/app/services/livros.service';

@Component({
  selector: 'app-livro-detalhes',
  templateUrl: './livro-detalhes.component.html',
  styleUrls: ['./livro-detalhes.component.css']
})
export class LivroDetalhesComponent implements OnInit {


  entity: Livro = <any>{};

  constructor(private route: ActivatedRoute, private service: LivroService) { }

  ngOnInit(): void {
    var id = this.route.snapshot.paramMap.get('id') as any;

    this.service.getLivro(id).toPromise()
      .then((r) => {
        this.entity = r;
      });
  }

  public parseData(input: any) {
    return new Date(input).toLocaleDateString();
  }
}
