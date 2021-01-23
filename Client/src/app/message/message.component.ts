import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { isNullOrUndefined } from 'util';
import { MessageService } from '../message.service';
import { MessageModel } from '../Models/MessageModel';
import { UserModel } from '../Models/UserModel';
import { SignalRService } from '../SignalRService/signalr.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  userList : UserModel[] = [];
  messageModel : MessageModel;
  msgList : MessageModel[] = [];
  receiver:any;
  message:string="";
  user : any;
  senderId : any;
  receiverMail : string;
  selectMsg : number;
  userName :string;
  
  constructor(private service:MessageService,
            private router:Router,
            private signalR:SignalRService) {
    this.user = localStorage.getItem("loginUserInfo");
    this.userName = JSON.parse(this.user).firstName;
    this.messageModel = new MessageModel();
   }

  ngOnInit() {
    if(isNullOrUndefined(this.user)){
     this.router.navigateByUrl("");   
    }
    this.getUserList();
    this.messageSendSignalFireMethod();
    this.messageDeleteSignalFireMethod();
  }

  getUserList():void{
    this.service.getUsers().subscribe((result:any)=>{
      this.userList = result.filter(x=>x.id !== JSON.parse(this.user).id);   
    });  
  }

  chatWithUser(user:any):void{
    this.receiver = user.id;
    this.senderId = JSON.parse(this.user).id;
    this.receiverMail = JSON.parse(this.user).email;
    this.service.getMsgList(this.senderId, user.id).subscribe((result:any)=>{
        this.msgList = result;

    });
  }

  sendMsg(msg:string):void{
    console.log(this.message);
    if(!isNullOrUndefined(msg) && msg.trim() !=='' && !isNullOrUndefined(this.receiver)){
      this.messageModel.SenderId = JSON.parse(this.user).id;
    this.messageModel.ReceiverId = this.receiver;
    this.messageModel.Text = msg;
    this.service.saveMessage(this.messageModel).subscribe(result =>{        
      this.message = null;
      this.getMessages();
    });
    }
    else if(isNullOrUndefined(this.receiver)){
      alert("Please select any friend.");
    }
    else{
      this.message = null;
    }
  }

  getMessages(){
    
    this.service.getMsgList(this.messageModel.SenderId, this.messageModel.ReceiverId).subscribe((result:any)=>{
      this.msgList = [];
      this.msgList = result;

  });
  }


  deleteMeMsg(msg:any){
    if(msg.senderId == JSON.parse(this.user).id){
        msg.IsDeletedBySender = true;
    }
    else{
      msg.isDeletedByReceiver = true;
    }
    this.service.deleteMessage(msg).subscribe(data=>{
      this.messageModel.SenderId = JSON.parse(this.user).id;
      this.messageModel.ReceiverId = this.receiver;
      this.getMessages();
    });
  }
  deleteAllMsg(msg:any){
    msg.isDeletedAll = true;
    this.service.deleteMessage(msg).subscribe(data=>{
      this.messageModel.SenderId = JSON.parse(this.user).id;
      this.messageModel.ReceiverId = this.receiver;
      this.getMessages();
    });
  }

  onRightClick(event,i) {
    this.selectMsg = i;
    return false;
  }
  onLeftClick(i){
    this.selectMsg = i;
    return false;
  }

  //SignalR Method
  messageSendSignalFireMethod(){
    this.signalR.MessageSendReceived.subscribe((signal:any)=>{
      // console.log(JSON.stringify(signal));
      if(signal == JSON.parse(this.user).id){
        this.messageModel.SenderId = signal;
        this.messageModel.ReceiverId = this.receiver;
        this.getMessages();
      }
    })
  }

  messageDeleteSignalFireMethod(){
    this.signalR.MessageDeleteReceived.subscribe((signal:any)=>{
      // console.log(JSON.stringify(signal));
      if(signal == JSON.parse(this.user).id){
        this.messageModel.SenderId = signal;
        this.messageModel.ReceiverId = this.receiver;
        this.getMessages();
      }
    })
  }


  //Logout
  Logout(){
    localStorage.removeItem("loginUserInfo");
    this.router.navigateByUrl("");
  }
 
}
