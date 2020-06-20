import { ProfileComponent } from './components/profile/profile.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { EditorComponent } from './components/editor/editor.component';
import { Role } from './models/role.model';


const accountModule = () => import('./components/account/account.module')
  .then(x => x.AccountModule);
const usersManagementModule = () => import('./components/admin/users_management/users-management.module')
  .then(x => x.UsersManagementModule);
const usersModule = () => import('./components/teacher/users/users.module')
  .then(x => x.UsersModule);
const taskModule = () => import('./components/teacher/tasks_management/tasks-management.module')
  .then(x => x.TasksManagementModule);

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'users-management',
    loadChildren: usersManagementModule,
    canActivate: [AuthGuard],
    data: { roles: [Role.Admin] }
  },
  {
    path: 'account',
    loadChildren: accountModule
  },
  {
    path: 'editor',
    component: EditorComponent
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'users',
    loadChildren: usersModule,
    canActivate: [AuthGuard],
    data: { roles: [Role.Teacher] }
  }, {
    path: 'tasks-management',
    loadChildren: taskModule,
    canActivate: [AuthGuard],
    data: { roles: [Role.Teacher] }
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
