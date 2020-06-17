import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { EditorComponent } from './components/editor/editor.component';


const accountModule = () => import('./components/account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./components/users/users.module').then(x => x.UsersModule);

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'users',
    loadChildren: usersModule,
    canActivate: [AuthGuard]
  },
  {
    path: 'account',
    loadChildren: accountModule
  },
  {
    path: 'editor',
    component: EditorComponent
  },
  // otherwise redirect to home
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
