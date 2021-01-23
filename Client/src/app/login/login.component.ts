import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  email:string;
  @ViewChild('form', {static: true}) public form:NgForm;
  
  constructor(private service : MessageService
    , private router : Router) { }

  ngOnInit() {
  }

  
  onSubmit() {
    if(this.form.valid){
      this.service.userLogin(this.email).subscribe(result=>{
          if(result != null){
              localStorage.setItem("loginUserInfo", JSON.stringify(result));
              this.router.navigateByUrl("/chat");
          }
          else{
            this.router.navigateByUrl("/login");
          }
      });
    }
    else{
      alert("Required");
    }
  }

  register(): void{
    this.router.navigateByUrl("/register");    
  }

}
