import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Game} from './model/game';
import {map, tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ChessService {
  private readonly localhost = 'https://localhost:5001';

  constructor(private _http: HttpClient) { }

  public newGame(players: number): Observable<number> {
    return this._http.get(`${this.localhost}/chess/games?players=${players}`).pipe(
      tap(x => console.log('new response', x)),
      map(value => value['id'])
    );
  }

  public get(id: number): Observable<Game> {
    return this._http.get<Game>(`${this.localhost}/chess/games/${id}`);
  }

  public getAvailable(id: number, x: number, y: number): Observable<any[]> {
    return this._http.post<any[]>(`${this.localhost}/chess/games/${id}/moves`, {id, x, y});
  }

  public move(id: number, fromX: number, fromY: number, toX: number, toY: number): Observable<any[]> {
    return this._http.post<any[]>(`${this.localhost}/chess/games/${id}/moves`,
      { from: { x: fromX, y: fromY }, to: { x: toX, y: toY } });
  }
}
