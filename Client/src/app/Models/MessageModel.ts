export class MessageModel{
    Id:number;
    Name : string;
    Text : string;
    SenderId : number;
    ReceiverId : number;  
    TextTime : Date;  
    IsDeletedBySender : boolean = false;
    IsDeletedByReceiver : boolean = false;
    IsDeletedAll : boolean = false;
}