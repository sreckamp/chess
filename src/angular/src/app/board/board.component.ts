import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Rotation} from '../model/rotation';
import {Piece} from '../model/piece';
import {PieceType} from '../model/piece.type';
import {Placement, Point} from '../model/placement';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: [
    './board.component.css',
    './board-meta.css',
    './board-black.css',
    './board-blue.css',
    './board-green.css',
    './board-orange.css',
    './board-red.css',
    './board-tan.css'
    ]
})
export class BoardComponent implements OnInit {
  public currentPlayer = 'white';

  @Input()
  public rotation = Rotation.NONE;

  @Input()
  public theme = 'tan';

  squares: boolean[][] = [];

  @Input()
  public pieces: Placement<Piece>[] = [];

  @Input()
  public selected: Point = new Point(-1, -1);

  @Input()
  public highlighted: Point[] = [];

  @Output()
  public readonly squareClicked = new EventEmitter<Point>();

  private _ranks: string[] = [];
  private _files: string[] = [];

  public get headers(): string[] {
    return [Rotation.CLOCKWISE, Rotation.COUNTERCLOCKWISE].includes(this.rotation) ? this._ranks : this._files;
  }

  public get labels(): string[] {
    return [Rotation.CLOCKWISE, Rotation.COUNTERCLOCKWISE].includes(this.rotation) ? this._files : this._ranks;
  }

  private _size = 8;
  @Input()
  public set config(value: [number, number]) {
    this._size = value[0];
    this._corner = value[1];
    this.generateSquares();
  }

  public get size(): number {
    return this._size;
  }

  private _corner = 0;

  public get corner(): number {
    return this._corner;
  }

  constructor() {
  }

  private isVisible(row: number, col: number): boolean {
    return (this.corner === 0) ||
      !((row < this.corner || row >= this.size - this.corner) &&
      (col < this.corner || col >= this.size - this.corner));
  }

  public isSelected(row: number, col: number): boolean {
    return this.selected.x === col && this.selected.y === row;
  }

  public isHighlighted(row: number, col: number): boolean {
    return this.highlighted.some(value => value.x === col && value.y === row);
  }

  public isOpponent(color: string): boolean {
    return false;
  }

  public isInCheck(row: number, col: number): boolean {
    return false;
  }

  public getPiece(row: number, col: number): Piece {
    const placement = this.pieces && this.pieces
      .find(value => value.location.y === row && value.location.x === col);
    return placement && placement.value || new Piece();
  }

  ngOnInit(): void {
    this.generateSquares();
  }

  private generateSquares(): void {
    const squares: any[][] = [];
    const files = [];
    const ranks = [];
    for (let idx = 0; idx < this._size; idx++) {
      files.push(String.fromCharCode('A'.charCodeAt(0) + idx));
      ranks.push('' + (idx + 1));

      const row = [];
      for (let col = 0; col < this._size; col++) {
        row.push(this.isVisible(idx, col));
      }

      squares.push(row);
    }
    this.squares = squares;
    this._ranks = ranks;
    this._files = files;
  }

  clickSquare(rowIdx: number, colIdx: number): void {
    if (this.squares && this.squares[rowIdx] && this.squares[rowIdx][colIdx]) {
      this.squareClicked.emit(new Point(colIdx, rowIdx));
    }
  }
}
