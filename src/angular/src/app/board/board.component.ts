import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Rotation } from '../model/rotation';
import { Piece } from '../model/piece';
import { Placement, Point } from '../model/placement';
import { Marker } from '../services/chess/model/game';
import { PieceType } from '../model/piece.type';

@Component({
    selector: 'app-board',
    templateUrl: './board.component.html',
    styleUrls: [
        './board.component.css',
        './board-black.css',
        './board-blue.css',
        './board-green.css',
        './board-orange.css',
        './board-red.css',
        './board-tan.css'
    ]
})
export class BoardComponent implements OnInit {
    @Input()
    public rotation = Rotation.NONE;

    @Input()
    public theme = 'tan';

    squares: boolean[][] = [];

    @Input()
    public pieces: Placement<Piece>[] = [];

    @Input()
    public markers: Placement<Marker[]>[] = [];

    @Input()
    public showMarkers = true;

    @Input()
    public selected: Point = new Point(-1, -1);

    @Input()
    public highlighted: Point[] = [];

    @Output()
    public readonly squareClicked = new EventEmitter<Point>();

    private _ranks: string[] = [];
    private _files: string[] = [];
    private _corner = 0;

    get headers(): string[] {
        return [Rotation.CLOCKWISE, Rotation.COUNTERCLOCKWISE].includes(this.rotation) ? this._ranks : this._files;
    }

    get labels(): string[] {
        return [Rotation.CLOCKWISE, Rotation.COUNTERCLOCKWISE].includes(this.rotation) ? this._files : this._ranks;
    }

    private _size = 8;

    @Input()
    public set config(value: [number, number]) {
        this._size = value[0];
        this._corner = value[1];
        this.generateSquares();
    }

    @Input()
    public isOpponent: (Piece) => boolean = () => false

    public isInCheck(x: number, y: number): boolean {
        const piece = this.pieces.find(value => value.location.x === x && value.location.y === y && value.value.type !== PieceType.EMPTY);
        const placement = this.markers.find(value => value.location.x === x && value.location.y === y);
        let markers = [] as Marker[];
        if (placement) {
            markers = placement.value;
        }
        return markers.some(value => value.type === 'check') && piece && piece.value.type === PieceType.KING;
    }

    @Input()
    public isSelectable: (x: number, y: number) => boolean = () => true

    public get size(): number {
        return this._size;
    }

    public get corner(): number {
        return this._corner;
    }

    constructor() {
    }

    isVisible(col: number, row: number): boolean {
        return (this.corner === 0) ||
            !((row < this.corner || row >= this.size - this.corner) &&
                (col < this.corner || col >= this.size - this.corner));
    }

    isSelected(col: number, row: number): boolean {
        return this.selected.x === col && this.selected.y === row;
    }

    isHighlighted(col: number, row: number): boolean {
        return this.highlighted.some(value => value.x === col && value.y === row);
    }

    getPiece(col: number, row: number): Piece {
        const placement = this.pieces && this.pieces
            .find(value => value.location.y === row && value.location.x === col);
        return placement && placement.value || new Piece();
    }

    getMarkers(col: number, row: number): Marker[] {
        const placement = this.markers && this.markers
            .find(value => value.location.y === row && value.location.x === col);
        return placement && placement.value || [];
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

    clickSquare(col: number, row: number): void {
        if (this.squares && this.squares[row] && this.squares[row][col]) {
            this.squareClicked.emit(new Point(col, row));
        }
    }
}
