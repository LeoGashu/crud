import { Component, OnInit } from '@angular/core';
import { PessoasService } from '../../services/pessoas.service';
import { PessoaModel } from '../../models/pessoa.model';

@Component({
  selector: 'app-pessoas',
  templateUrl: './pessoas.component.html',
  styleUrls: ['./pessoas.component.css']
})
export class PessoasComponent implements OnInit {
  public pessoaLst: PessoaModel[] = null;

  constructor(private pessoasService: PessoasService) {
    this.getPessoas(true, "");
  }

  ngOnInit() {
  }

  public getPessoas(ativo: boolean, nome: string) {
    this.pessoasService.getPessoas(ativo, nome).subscribe(result => {
      console.log(result);
      this.pessoaLst = result;
    }, error => {
      console.error(error)
    });;
  }
}
