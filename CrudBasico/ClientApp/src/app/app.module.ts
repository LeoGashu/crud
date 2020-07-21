import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CadastrosComponent } from './cadastros/cadastros.component';
import { PessoasComponent } from './cadastros/pessoas/pessoas.component';
import { PessoaComponent } from './pessoa/pessoa.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OnlyNumbersDirective } from './directives/only-numbers.directive';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CadastrosComponent,
    PessoasComponent,
    PessoaComponent,
    OnlyNumbersDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'cadastros', component: CadastrosComponent },
      { path: 'cadastros/pessoas', component: PessoasComponent },
      { path: 'pessoa', component: PessoaComponent },
      { path: 'pessoa/:id', component: PessoaComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
