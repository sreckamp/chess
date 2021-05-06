import { Component, OnInit } from '@angular/core';
import { PieceType } from '../model/piece.type';
import { Rotation } from '../model/rotation';
import { Placement, Point } from '../model/placement';
import { Piece } from '../model/piece';
import { Color } from '../model/color';
import { Marker } from '../model/marker';
import { Direction } from '../model/direction';
import { MarkerType } from '../model/marker.type';
import { ChessService } from '../services/chess/chess.service';
import { GameTranslationService } from '../services/game.translation.service';
import { Game } from '../model/game';

@Component({
    selector: 'app-evaluator',
    templateUrl: './evaluator.component.html',
    styleUrls: ['./evaluator.component.css']
})
export class EvaluatorComponent implements OnInit {
    private _game = {
        id: 1234,
        size: 8,
        corners: 0,
        pieces: [{location: {x: 3, y: 3}, value: {color: Color.BLACK, type: PieceType.KING}} as Placement<Piece>],
        markers: [
            {location: {x: 4, y: 4}, value: [
                    {type: MarkerType.ENPASSANT, direction: Direction.NONE, source: {color: Color.WHITE, type: PieceType.PAWN}}
                ]},
            {location: {x: 5, y: 5}, value: [
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.WHITE, type: PieceType.KNIGHT}},
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.SILVER, type: PieceType.KNIGHT}},
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.BLACK, type: PieceType.KNIGHT}},
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.GOLD, type: PieceType.KNIGHT}}
                ]},
            {location: {x: 7, y: 5}, value: [
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.SILVER, type: PieceType.KNIGHT}},
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.BLACK, type: PieceType.KNIGHT}},
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.GOLD, type: PieceType.KNIGHT}}
                ]},
            {location: {x: 6, y: 6}, value: [
                    {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.WHITE, type: PieceType.KNIGHT}}
                ]},
            {location: {x: 2, y: 6}, value: [
                    {type: MarkerType.COVER, direction: Direction.NORTHWEST, source: {color: Color.WHITE, type: PieceType.BISHOP}}
                ]},
            {location: {x: 3, y: 6}, value: [
                    {type: MarkerType.CHECK, direction: Direction.NORTHWEST, source: {color: Color.WHITE, type: PieceType.BISHOP}},
                    {type: MarkerType.COVER, direction: Direction.NORTHWEST, source: {color: Color.BLACK, type: PieceType.KING}}
                ]},
            {location: {x: 3, y: 5}, value: [
                    {type: MarkerType.PIN, direction: Direction.NORTHWEST, source: {color: Color.GOLD, type: PieceType.BISHOP}},
                    {type: MarkerType.COVER, direction: Direction.NORTHWEST, source: {color: Color.BLACK, type: PieceType.KING}}
                ]},
            {location: {x: 2, y: 5}, value: [
                    {type: MarkerType.PIN, direction: Direction.NORTH, source: {color: Color.GOLD, type: PieceType.QUEEN}}
                ]},
            {location: {x: 4, y: 5}, value: [
                    {type: MarkerType.CHECK, direction: Direction.NORTHEAST, source: {color: Color.WHITE, type: PieceType.QUEEN}},
                    {type: MarkerType.COVER, direction: Direction.NORTHEAST, source: {color: Color.WHITE, type: PieceType.BISHOP}}
                ]}
        ]
    } as Game;
    private _config: [number, number] = [8, 0];

    set config(value: [number, number]) {
        this._config = value;
        [this._game.size, this._game.corners] = value;
    }

    get config(): [number, number] {
        return this._config;
    }

    playerCount: number;
    rotation = Rotation.NONE;
    rotations = Rotation;

    get pieces(): Placement<Piece>[] {
        return this._game.pieces;
    }

    get markers(): Placement<Marker[]>[] {
        return this._game.markers;
    }

    rotationKeys = [];
    selected = new Point(-1, -1);
    highlighted = [];

    private _activePiece = new Piece();

    constructor(private _service: ChessService, private _translator: GameTranslationService) {
        this.rotationKeys = Object.keys(this.rotations);
    }

    ngOnInit(): void {
    }

    changeRotation(rotation: Rotation): void {
        this.rotation = rotation;
    }

    changePlayers(players: number): void {
        this.playerCount = players;
        this.config = players === 4 ? [14, 3] : [8, 0];
    }

    clickHandler(point: Point): void {
        const placement = this.getPlacement(point);
        if (placement.value.type !== this._activePiece.type || placement.value.color !== this._activePiece.color) {
            placement.value.type = this._activePiece.type;
            placement.value.color = this._activePiece.color;
        } else {
            this._game.pieces = this.pieces.filter(value => value.location.x !== point.x || value.location.y !== point.y);
        }
    }

    getPlacement(point: Point): Placement<Piece> {
        let placement = this.pieces.find(value => value.location.x === point.x && value.location.y === point.y);
        if (!placement) {
            placement = new Placement<Piece>(point.x, point.y, new Piece());
            this.pieces.push(placement);
        }
        return placement;
    }

    isSelectable = (_x: number, _y: number) => {
        return this._activePiece && this._activePiece.type !== PieceType.EMPTY;
    }

    pieceSelected($event: Piece): void {
        this._activePiece = $event || new Piece();
    }

    submit(_: MouseEvent): void {
        this._service.evaluate(this._translator.toApi(this._game)).subscribe(value => {
            this._game = this._translator.fromApi(value);
        });
    }
}
