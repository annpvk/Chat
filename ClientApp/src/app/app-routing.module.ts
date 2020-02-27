import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: 'chats', component: ChatComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: '**', redirectTo: 'chats' }
];

import { ChatComponent } from './modules/chat/pages/chat.component';
import { LoginComponent } from './shared/components/login.component';
import { RegisterComponent } from './core/register/register.component';
import { AuthGuard } from './core/helpers/auth.guard';

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }