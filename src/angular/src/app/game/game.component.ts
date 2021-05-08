import { Component, OnInit } from '@angular/core';
import { Rotation } from '../model/rotation';
import { Placement, Point } from '../model/placement';
import { Piece } from '../model/piece';
import { Color } from '../model/color';
import { ChessService } from '../services/chess/chess.service';
import { GameTranslationService } from '../services/game.translation.service';
import { Game } from '../model/game';
import { Marker } from '../model/marker';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    private _game = {
        size: 8,
        corners: 0,
        activeColor: Color.NONE,
        pieces: [],
        markers: []
    } as Game;

    get id(): number {
        return this._game.id;
    }

    get pieces(): Placement<Piece>[] {
        return this._game.pieces;
    }

    get markers(): Placement<Marker[]>[] {
        return this._game.markers;
    }

    private _config: [number, number] = [8, 0];

    set config(value: [number, number]) {
        this._config = value;
        [this._game.size, this._game.corners] = value;
    }

    get config(): [number, number] {
        return this._config;
    }

    rotation = Rotation.NONE;
    rotations = Rotation;
    showMarkers = false;
    rotationKeys = [];
    selected = new Point(-1, -1);
    highlighted = [];

    constructor(private _service: ChessService,
                private _translator: GameTranslationService,
                private _route: ActivatedRoute) {
        this.rotationKeys = Object.keys(this.rotations);
    }

    ngOnInit(): void {
        this._game.id = +this._route.snapshot.params.id;
        this._service.get(this._game.id).subscribe(value => {
            this._game = this._translator.fromApi(value);
            this.config = [value.size, value.corners];
        });
    }

    changeRotation(rotation: Rotation): void {
        this.rotation = rotation;
    }

    changePlayers(players: number): void {
        this.config = players === 4 ? [14, 3] : [8, 0];
    }

    clickHandler(point: Point): void {
        if (point.x === this.selected.x && point.y === this.selected.y) {
            this.highlighted = [];
            this.selected = new Point(-1, -1);
        } else if (this.highlighted.some(value => value.x === point.x && value.y === point.y)) {
            this._service.move(this._game.id, this.selected, point).subscribe(value => {
                this._game = this._translator.fromApi(value);
                this.highlighted = [];
                this.selected = new Point(-1, -1);
            });
        } else {
            const clickedPiece = this.getPiece(point);
            if (this._game.activeColor === clickedPiece.color) {
                this.selected = point;
                this._service.getAvailable(this._game.id, point).subscribe(value => this.highlighted = value);
            }
        }
    }

    isSelectable = (x: number, y: number) => this.getPiece(new Point(x, y)).color === this._game.activeColor;

    isOpponent = (piece: Piece) => piece.color !== Color.NONE && piece.color !== this._game.activeColor;

    private getPiece(point: Point): Piece {
        const placement = this._game.pieces.find(value => value.location.x === point.x && value.location.y === point.y);
        return placement && placement.value || new Piece();
    }
}
