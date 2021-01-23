import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { UserModel } from './Models/UserModel';
import { MessageModel } from './Models/MessageModel';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  
  private baseUrl = 'https://localhost:44347/api'

  constructor(private httpClient: HttpClient) { }

  //#region User
userLogin(email:string){
  return this.httpClient.get(this.baseUrl+"/User/Login?email=" + email);
}

getUsers(){
  return this.httpClient.get(this.baseUrl+"/User");
}

createUser(userModel : UserModel){
  return this.httpClient.post(this.baseUrl+"/User/CreateUser", userModel);
}
//#endregion

//#region Message
saveMessage(message : MessageModel){
  return this.httpClient.post(this.baseUrl+"/Message/SaveMessage", message);
}

getMsgList(senderId:number, receiverId:number){
  return this.httpClient.get(this.baseUrl+"/Message/GetMessages?senderId="+senderId+"&receiverId="+receiverId);
}

deleteMessage(message : MessageModel){
  return this.httpClient.post(this.baseUrl + "/Message/DeleteMessage", message);
}
//#endregion

}
