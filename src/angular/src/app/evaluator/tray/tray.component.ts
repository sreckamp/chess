import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Point } from '../../model/placement';
import { PieceType } from '../../model/piece.type';
import { Color } from '../../model/color';
import { Piece } from '../../model/piece';

@Component({
    selector: 'app-tray',
    templateUrl: './tray.component.html',
    styleUrls: ['./tray.component.css']
})
export class TrayComponent implements OnInit {
    private static readonly _initialPower =
        [PieceType.KING, PieceType.QUEEN, PieceType.BISHOP, PieceType.KNIGHT, PieceType.ROOK, PieceType.PAWN];
    private static readonly _initialColors = [Color.WHITE, Color.BLACK, Color.SILVER, Color.GOLD];

    private _playerCount = 2;

    @Input()
    public set playerCount(value: number) {
        this._playerCount = value;
        this.populate(this._playerCount);
    }

    public get playerCount(): number {
        return this._playerCount;
    }

    @Output()
    public pieceSelected: EventEmitter<Piece> = new EventEmitter<Piece>();

    squares: Piece[][] = [];
    public selected: Point = new Point(-1, -1);

    constructor() {
    }

    ngOnInit(): void {
        this.populate(2);
    }

    populate(players: number): void {
        const temp = [] as Piece[][];
        for (let y = 0; y < players; y++) {
            const row = TrayComponent._initialPower.map(value => ({
                color: TrayComponent._initialColors[y],
                type: value
            } as Piece));
            temp.push(row);
        }
        this.squares = temp;
    }

    clickSquare(x: number, y: number): void {
        this.selected = new Point(x, y);
        this.pieceSelected.emit(this.squares[y][x]);
    }
}
