import { Inject, Injectable, InjectionToken } from '@angular/core';

export const LOCAL_STORAGE = new InjectionToken<Storage>('Local Storage', {
    providedIn: 'root',
    factory: () => localStorage
});

export const SESSION_STORAGE = new InjectionToken<Storage>('Session Storage', {
    providedIn: 'root',
    factory: () => sessionStorage
});

@Injectable({
    providedIn: 'root'
})
export class BlockedStorageService {
    constructor(@Inject(LOCAL_STORAGE) private _local: Storage, @Inject(SESSION_STORAGE) private _session: Storage) {
    }

    public getConnectionId(gameId: number, current: string): string {
        let connectionId = this.read<string>(gameId.toString(), 'connectionId');
        if(!connectionId) {
            connectionId = current;
            this.write(gameId.toString(), 'connectionId', connectionId);
        }
        return connectionId;
    }

    private read<T>(blockKey: string, key: string): T {
        const block = JSON.parse(this._session.getItem(blockKey)) || {};
        return block[key] as T;
    }

    private write<T>(blockKey: string, key: string, value: T) {
        const block = JSON.parse(this._session.getItem(blockKey)) || {};
        block[key] = value;
        this._session.setItem(blockKey, JSON.stringify(block));
    }
}
