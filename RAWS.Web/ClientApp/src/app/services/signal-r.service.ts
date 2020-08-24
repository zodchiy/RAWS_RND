import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Symboltiket } from '../symboltiket';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: Symboltiket[];

  private hubConnection: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .configureLogging(signalR.LogLevel.Debug)
                            .withUrl('https://localhost:32824/message')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }
  public addTransferChartDataListener = () => {
    this.hubConnection.on('binance_quotas', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}
