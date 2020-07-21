import { Component, OnInit, Type } from '@angular/core';
import { Location } from '@angular/common';
import { PessoasService } from '../services/pessoas.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { PessoaModel } from '../models/pessoa.model';
import { Guid } from 'guid-typescript'
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Utility } from '../services/utility.service';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
  styleUrls: ['./pessoa.component.css']
})
export class PessoaComponent implements OnInit {
  id: string = Guid.createEmpty().toString();
  pessoaForm: FormGroup;
  closeResult = '';

  constructor(private pessoasService: PessoasService, private route: ActivatedRoute, private formBuilder: FormBuilder, private location: Location, private modalService: NgbModal, private utility: Utility) {
    this.pessoaForm = this.formBuilder.group({
      id: [this.id],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      nome: ['', Validators.required],
      endereco: ['', Validators.required],
      telefone: ['', Validators.required],
      cpf: ['', Validators.compose([Validators.required, Validators.minLength(11), Validators.maxLength(11), this.utility.validateCpf])],
      ativo: [true, Validators.required]
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const paramId = params.get('id');
      if (paramId !== null && paramId !== undefined && Guid.isGuid(paramId)) {
        this.id = paramId;
        this.pessoasService.getPessoa(this.id).subscribe(result => {
          this.pessoaForm.patchValue(result);
        }, error => {
          console.error(error);
        })
      }
    });
  }

  onSubmit(pessoaForm: PessoaModel) {
    this.pessoasService.postPessoa(pessoaForm).subscribe(result => {
      this.location.back();
    }, error => {
      console.error(error);
    });
  }

  isGuidEmpty(guid: string): boolean {
    return Guid.EMPTY === guid;
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      console.log(result);
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  deletarPessoa() {
    this.pessoasService.deletePessoa(this.id).subscribe(result => {
      if (result > 0) {
        this.location.back();
      }
    }, error => {
      console.error(error);
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
