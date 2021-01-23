import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MessageComponent } from './message/message.component';
import { RegisterComponent } from './register/register.component';


const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  {path:'chat', component: MessageComponent},
  {path:'login', component: LoginComponent},
    // otherwise redirect to home
    { path: '**', redirectTo: '/login', pathMatch:'full' }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forRoot(routes)],

    declarations: [
      RegisterComponent
     , MessageComponent
     , LoginComponent         
      
    ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
