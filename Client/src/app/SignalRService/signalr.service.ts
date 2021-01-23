import { EventEmitter, Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";

@Injectable({
    providedIn : "root"
})

export class SignalRService{

private hubConnection : signalR.HubConnection;

MessageSendReceived = new EventEmitter<any>();
MessageDeleteReceived = new EventEmitter<any>();

constructor() {
    this.buildConnection();
    this.startConnection();
}

private buildConnection = () =>{
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl("https://localhost:44347/"+"signalHub")
                            .build();

}

public stopConnection(){
    console.log("Stop Connection");
    this.hubConnection.stop().then(()=>{
        console.log("Connection Stopped");
    });
}

private startConnection = () =>{
this.hubConnection.start()
                .then(()=>{
                    this.callConnectionEvents();
                })
                .catch(err => {
                    setTimeout(function(){
                        this.startConnection();
                    }, 3000);
                })


}

callConnectionEvents(){
this.messageSendEvents();
this.messageDeleteEvents();
}

private messageSendEvents(){
    this.hubConnection.on("MessageSave", (data:any)=>{
        this.MessageSendReceived.emit(data);
    })
}

private messageDeleteEvents(){
    this.hubConnection.on("MessageRemove", (data:any)=>{
        this.MessageDeleteReceived.emit(data);
    })
}


}