import { Component, OnInit } from '@angular/core';
import { PessoasService } from '../services/pessoas.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { PessoaModel } from '../models/pessoa.model';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
  styleUrls: ['./pessoa.component.css']
})
export class PessoaComponent implements OnInit {
  id: string;
  pessoaForm: FormGroup;

  constructor(private pessoasService: PessoasService, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.pessoaForm = this.formBuilder.group({
      id: [],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      nome: ['', Validators.required],
      endereco: ['', Validators.required],
      telefone: ['', Validators.required],
      cpf: ['', Validators.required],
      ativo: [true, Validators.required]
    })
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if (this.id !== undefined && this.id !== "") {
        this.pessoasService.getPessoa(this.id).subscribe(result => {
          this.pessoaForm.patchValue(result);
        }, error => {
          console.error(error);
        })
      }
    });
  }

  onSubmit(pessoaForm: PessoaModel) {
    console.log(pessoaForm);
    this.pessoasService.postPessoa(pessoaForm).subscribe(result => {
      console.log(result);
    }, error => {
      console.error(error);
    });
  }
}
