import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, Subscription } from 'rxjs';
import { GameUpdateMessage } from './model/game.update.message';
import { HubConnectionState } from '@microsoft/signalr';
import { filter } from 'rxjs/operators';

@Injectable()
export class EventService {
    // https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-5.0
    // https://www.c-sharpcorner.com/article/real-time-angular-11-application-with-signalr-and-net-5/
    // https://medium.com/@dbottiau/simplify-your-realtime-applications-by-bringing-signalr-into-the-ngrx-ecosystem-bc984cf2800c
    private _connection: signalR.HubConnection;
    private _receiver: Subject<number> = new Subject<number>();

    constructor() {
        this._connection = new signalR.HubConnectionBuilder()
            .withUrl(`${window.location.protocol}//${window.location.host}/api/events`)
            // .configureLogging(signalR.LogLevel.Trace)
            .build();
        this.connect();
    }

    private connect(): void {
        if ([HubConnectionState.Disconnecting, HubConnectionState.Disconnected].includes(this._connection.state)) {
            this._connection.start();
            this._connection.on('GameUpdated', (args: GameUpdateMessage) =>
                this._receiver.next(args.id));
        }
    }

    public onGameUpdated(id: number, subscription: (_: number) => void): Subscription {
        return this._receiver.asObservable().pipe(
            filter(value => value === id)
        ).subscribe(subscription);
    }
}
