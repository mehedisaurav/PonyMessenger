import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';
import { UserModel } from '../Models/UserModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  @ViewChild('form', {static: true}) public form:NgForm;
  userModel : UserModel;
  constructor(private service : MessageService, private router : Router) { 
  this.userModel = new UserModel();

  }

  ngOnInit() {
  }

  onSubmit() {
    if(this.form.valid){
      this.service.createUser(this.userModel).subscribe(result=>{
          if(!result){

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

  cancel(): void{
    this.router.navigateByUrl("/login");    
  }
}
