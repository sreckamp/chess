import {Component, OnInit} from '@angular/core';
import {PieceType} from '../model/piece.type';
import {Rotation} from '../model/rotation';
import {Placement, Point} from '../model/placement';
import {Piece} from '../model/piece';
import {Color} from '../model/color';
import {Marker} from '../model/marker';
import {Direction} from '../model/direction';
import {MarkerType} from '../model/marker.type';
import {ChessService} from '../services/chess/chess.service';
import {GameTranslationService} from '../services/game.translation.service';
import {Game} from '../model/game';

@Component({
    selector: 'app-evaluator',
    templateUrl: './evaluator.component.html',
    styleUrls: ['./evaluator.component.css']
})
export class EvaluatorComponent implements OnInit {
    private _game = {
        activeColor: Color.NONE,
        id: 0,
        size: 8,
        corners: 0,
        pieces: [],
        markers: []
    } as Game;
    private _config: [number, number] = [8, 0];

    set config(value: [number, number]) {
        this._config = value;
        [this._game.size, this._game.corners] = value;
    }

    get config(): [number, number] {
        return this._config;
    }

    get activeColor(): Color {
        return this._game.activeColor;
    }

    playerCount: number;
    rotation = Rotation.NONE;
    rotations = Rotation;

    colors = Color;

    get pieces(): Placement<Piece>[] {
        return this._game.pieces;
    }

    get markers(): Placement<Marker[]>[] {
        return this._game.markers;
    }

    rotationKeys = [];
    colorKeys = [];
    selected = new Point(-1, -1);
    highlighted = [];

    private _activePiece = new Piece();

    constructor(private _service: ChessService, private _translator: GameTranslationService) {
        this.rotationKeys = Object.keys(this.rotations);
        this.colorKeys = Object.keys(this.colors);
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
            this._game.markers = [];
        } else {
            this._game.pieces = this.pieces.filter(value => value.location.x !== point.x || value.location.y !== point.y);
            this._game.markers = [];
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

    changeActiveColor(color: Color): void {
        this._game.activeColor = color;
    }
}
