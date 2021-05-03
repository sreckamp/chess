import { Component, OnInit } from '@angular/core';
import {PieceType} from '../model/piece.type';
import {Rotation} from '../model/rotation';
import {Placement, Point} from '../model/placement';
import {Piece} from '../model/piece';
import {Color} from '../model/color';
import {ChessService} from '../services/chess.service';
import {concatMap, switchMap, tap} from 'rxjs/operators';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  private readonly _initialPower = [PieceType.ROOK, PieceType.KNIGHT, PieceType.BISHOP, PieceType.QUEEN,
    PieceType.KING, PieceType.BISHOP, PieceType.KNIGHT, PieceType.ROOK];

  config: [number, number] = [8, 0];
  rotation = Rotation.NONE;
  rotations = Rotation;
  pieces: Placement<Piece>[] = [];
  rotationKeys = [];
  selected = new Point(-1, -1);
  highlighted = [new Point(4, 2), new Point(4, 3), new Point(4, 4), new Point(4, 5)];

  private _id: number;
  constructor(private _service: ChessService) {
    this.rotationKeys = Object.keys(this.rotations);
  }

  ngOnInit(): void {
    this.populate(2);
    this._service.newGame(2).pipe(
      tap(value => this._id = value),
      concatMap(value => this._service.get(value).pipe(
        tap(x => console.log('game state', x))
      ))
    ).subscribe();
  }

  changeRotation(rotation: Rotation): void {
    this.rotation = rotation;
  }

  changePlayers(players: number): void {
    this.config = players === 4 ? [14, 3] : [8, 0];
    this.populate(players);
  }

  private populate(players: number): void {
    const pieces = [];
    for (let idx = 0; idx < this._initialPower.length; idx++) {
      pieces.push(this.createPlacement(this.config[1] + idx, this.config[0] - 2, Color.BLACK, PieceType.PAWN));
      pieces.push(this.createPlacement(this.config[1] + idx, this.config[0] - 1, Color.BLACK, this._initialPower[idx]));
      if (players < 4) {
        pieces.push(this.createPlacement(this.config[1] + idx, 1, Color.WHITE, PieceType.PAWN));
        pieces.push(this.createPlacement(this.config[1] + idx, 0, Color.WHITE, this._initialPower[idx]));
      } else {
        const reverseIdx = this._initialPower.length - 1 - idx;
        pieces.push(this.createPlacement(this.config[1] + idx, 0, Color.WHITE, this._initialPower[reverseIdx]));
        pieces.push(this.createPlacement(this.config[1] + idx, 1, Color.WHITE, PieceType.PAWN));
        pieces.push(this.createPlacement(0, this.config[1] + idx, Color.SILVER, this._initialPower[idx]));
        pieces.push(this.createPlacement(1, this.config[1] + idx, Color.SILVER, PieceType.PAWN));
        pieces.push(this.createPlacement(this.config[0] - 1, this.config[1] + idx,
          Color.GOLD, this._initialPower[reverseIdx]));
        pieces.push(this.createPlacement(this.config[0] - 2, this.config[1] + idx,
          Color.GOLD, PieceType.PAWN));
      }
    }
    this.pieces = pieces;
  }

  private createPlacement(x: number, y: number, color: Color, type: PieceType): Placement<Piece> {
    return new Placement<Piece>(x, y, { color, type } as Piece);
  }

  clickHandler(point: Point): void {
    console.log(`click! (${point.x}, ${point.y})`);
    this.selected = point;
  }
}
