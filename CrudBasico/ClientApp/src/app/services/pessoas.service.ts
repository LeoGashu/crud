import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PessoaModel } from '../models/pessoa.model';

@Injectable({
  providedIn: 'root'
})
export class PessoasService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  getPessoas(ativo: boolean, nome: string): Observable<PessoaModel[]> {
    return this.http.get<PessoaModel[]>(this.baseUrl + 'api/pessoas?ativo=' + ativo + "&nome=" + nome);
  }

  getPessoa(id: string): Observable<PessoaModel> {
    return this.http.get<PessoaModel>(this.baseUrl + 'api/pessoas/' + id);
  }

  postPessoa(pessoa: PessoaModel): Observable<PessoaModel> {
    return this.http.post<PessoaModel>(this.baseUrl + 'api/pessoas', pessoa);
  }
}
