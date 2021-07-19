import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { Livro } from '../domain/livro';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  constructor(private http: HttpClient) { }

  public getLivros(palavraChave: string, dataInicial: Date, dataFinal: Date): Observable<any> {

    var params = new HttpParams()
    .set('PalavraChave', palavraChave)
    .set('DataInicio',  (dataInicial != undefined) ? dataInicial.toISOString() : "")
    .set('DataFim', (dataFinal != undefined) ? dataFinal.toISOString() : "");
    
    return this.http.get("https://localhost:5001/api/v1/livro?" + params.toString());
  }

  public cadastrarLivro(entity : Livro) : Observable<any> {
    return this.http.post("https://localhost:5001/api/v1/livro/", entity);
  }

  public atualizarLivro(entity : Livro) : Observable<any> {
    return this.http.put("https://localhost:5001/api/v1/livro/" + entity.id.toString(), entity);
  }

  public excluirLivro(id : string) : Observable<any> {
    return this.http.delete("https://localhost:5001/api/v1/livro/ " + id);
  }


  public getLivro(id: string) : Observable<any>
  {
    return this.http.get("https://localhost:5001/api/v1/livro/" + id.toString())
  }
}
