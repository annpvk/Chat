import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: 'chats', component: ChatComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'create', component: CreateChatComponent, canActivate: [AuthGuard] },
  { path: 'searchUser', component: UsersSearchComponent, canActivate: [AuthGuard] },

  { path: '**', redirectTo: 'chats' }
];

import { ChatComponent } from './modules/chat/pages/chat.component';
import { LoginComponent } from './shared/components/login.component';
import { RegisterComponent } from './core/register/register.component';
import { AuthGuard } from './core/helpers/auth.guard';
import { ChatDetailComponent } from './modules/chat/components/chat-details/chat-detail.component';

import { SignalrService } from './core/signalR/SignalR.service';
import { CreateChatComponent } from './modules/chat/pages/create-chat/create-chat.component';
import { UsersSearchComponent } from './shared/components/users-search/users-search.component';

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
