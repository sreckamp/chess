import { Component, OnInit } from '@angular/core';
import { parsePieceType } from '../model/piece.type';
import { Rotation } from '../model/rotation';
import { Placement, Point } from '../model/placement';
import { Piece } from '../model/piece';
import { Color, parseColor } from '../model/color';
import { ChessService } from '../services/chess/chess.service';
import { concatMap, tap } from 'rxjs/operators';
import { Game as ApiGame, Marker as ApiMarker, Piece as ApiPiece } from '../services/chess/model/game';
import { Marker } from '../model/marker';
import { parseDirection } from '../model/direction';
import { parseMarkerType } from '../model/marker.type';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    config: [number, number] = [8, 0];
    rotation = Rotation.NONE;
    rotations = Rotation;
    pieces: Placement<Piece>[] = [];
    markers: Placement<Marker[]>[] = [];
    showMarkers = true;
    rotationKeys = [];
    selected = new Point(-1, -1);
    highlighted = [];
    id: number;
    name: string;

    private _activeColor: Color;

    constructor(private _service: ChessService) {
        this.rotationKeys = Object.keys(this.rotations);
    }

    ngOnInit(): void {
        this._service.newGame(2).pipe(
            tap(value => this.id = value),
            concatMap(value => this._service.get(value))
        ).subscribe(x => this.parseGame(x));
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
            this._service.move(this.id, this.selected, point).subscribe(value => {
                this.parseGame(value);
                this.highlighted = [];
                this.selected = new Point(-1, -1);
            });
        } else {
            const clickedPiece = this.getPiece(point);
            if (this._activeColor === clickedPiece.color) {
                this.selected = point;
                this._service.getAvailable(this.id, point).subscribe(value => this.highlighted = value);
            }
        }
    }

    isSelectable(x: number, y: number): boolean {
        return this.getPiece(new Point(x, y)).color === this._activeColor;
    }

    isOpponent(piece: Piece): boolean {
        return piece.color !== Color.NONE && piece.color !== this._activeColor;
    }

    private getPiece(point: Point): Piece {
        const placement = this.pieces.find(value => value.location.x === point.x && value.location.y === point.y);
        return placement && placement.value || new Piece();
    }

    private parseGame(game: ApiGame): void {
        this.id = game.gameId;
        this.config = [game.size, game.corners];
        this.name = game.name;
        this._activeColor = game.currentPlayer && parseColor(game.currentPlayer) || Color.NONE;
        this.pieces = game.pieces
            .map(value => {
                const piece = {
                    color: parseColor(value.color),
                    type: parsePieceType(value.type)
                } as Piece;
                return new Placement<Piece>(value.location.x, value.location.y, piece);
            }).filter(value => value.value.color !== Color.NONE);
        this.markers = game.pieces
            .map((apiPiece: ApiPiece) => {
                const markers: Marker[] = apiPiece.location.metadata.markers.map((marker: ApiMarker) => {
                    return {
                        direction: parseDirection(marker.direction),
                        type: parseMarkerType(marker.type),
                        source: {type: parsePieceType(marker.sourceType), color: parseColor(marker.sourceColor)} as Piece
                    } as Marker;
                });
                return new Placement<Marker[]>(apiPiece.location.x, apiPiece.location.y, markers);
            }).filter(value => value.value.length > 0);
    }
}
