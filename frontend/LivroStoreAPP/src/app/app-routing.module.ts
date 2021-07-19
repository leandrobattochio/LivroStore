import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LivroDetalhesComponent } from './pages/livro-detalhes/livro-detalhes.component';
import { ListaLivrosComponent } from './pages/lista-livros/lista-livros.component';
import { GerenciarLivrosComponent } from './pages/gerenciar-livros/gerenciar-livros.component';
import { RegistroComponent } from './pages/registro/registro.component';
import { LoginComponent } from './pages/login/login.component';
import { LoggedinGuard } from './guards/loggedin.guard';

const routes: Routes = [
  {
    path: 'lista-livros', component: ListaLivrosComponent, canActivate: [ LoggedinGuard ]
  },
  {
    path: 'livro-detalhes', component: LivroDetalhesComponent, canActivate: [ LoggedinGuard ]
  },
  {
    path: 'gerenciar-livros', component: GerenciarLivrosComponent, canActivate: [ LoggedinGuard ]
  },
  {
    path: 'registro', component: RegistroComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: '', redirectTo: '/login', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
