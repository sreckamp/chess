import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from './model/game';
import { map } from 'rxjs/operators';
import { Point } from '../../model/placement';
import { GameSummary } from './model/game.summary';

@Injectable({
    providedIn: 'root'
})
export class ChessService {
    private readonly API_BASE = '/api';
    constructor(private _http: HttpClient) {
    }

    public newGame(players: number): Observable<number> {
        return this._http.post<HttpResponse<any>>(`${this.API_BASE}/games`, {players}, {observe: 'response'})
            .pipe(
                map(response => +response.headers.get('location').split('/').pop())
            );
    }

    public list(): Observable<GameSummary[]> {
        return this._http.get<GameSummary[]>(`${this.API_BASE}/games`);
    }

    public get(id: number): Observable<Game> {
        return this._http.get<Game>(`${this.API_BASE}/games/${id}`);
    }

    public getAvailable(id: number, point: Point): Observable<Point[]> {
        return this._http.get<any[]>(`${this.API_BASE}/games/${id}/moves?x=${point.x}&y=${point.y}`).pipe(
            map(value => value.map(location => ({x: location.x, y: location.y})))
        );
    }

    public move(id: number, from: Point, to: Point): Observable<Game> {
        return this._http.post<Game>(`${this.API_BASE}/games/${id}/moves`,
            {from: {x: from.x, y: from.y}, to: {x: to.x, y: to.y}});
    }

    public evaluate(game: Game): Observable<Game> {
        return this._http.post<Game>(`${this.API_BASE}/evaluate`, game);
    }
}
