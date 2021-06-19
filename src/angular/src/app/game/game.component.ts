import { Component, OnInit } from '@angular/core';
import { Rotation } from '../model/rotation';
import { Placement, Point } from '../model/placement';
import { Piece } from '../model/piece';
import { Color, parseColor } from '../model/color';
import { ChessService } from '../services/chess/chess.service';
import { GameTranslationService } from '../services/game.translation.service';
import { Game } from '../model/game';
import { Marker } from '../model/marker';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../services/event/event.service';
import { OnPageVisibilityChange } from 'angular-page-visibility';
import { TitleBlinkerService } from '../services/title.blinker/title.blinker.service';
import { switchMap } from "rxjs/operators";
import { BlockedStorageService } from "../services/storage/blocked.storage.service";

/**
 * https://developer.mozilla.org/en-US/docs/Web/API/Page_Visibility_API
 * https://www.npmjs.com/package/angular-page-visibility
 * https://stackoverflow.com/questions/46751656/using-visibility-api-in-angular
 */
@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css'],
    providers: [TitleBlinkerService]
})
export class GameComponent implements OnInit {
    private _rotationMap = {
        none: Rotation.NONE,
        white: Rotation.NONE,
        silver: Rotation.COUNTERCLOCKWISE,
        black: Rotation.UPSIDEDOWN,
        gold: Rotation.CLOCKWISE
    }
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
    showMarkers = false;
    selected = new Point(-1, -1);
    highlighted = [];
    private _color = Color.NONE;

    constructor(private _service: ChessService,
                private _translator: GameTranslationService,
                private _route: ActivatedRoute,
                private _events: EventService,
                private _storage: BlockedStorageService,
                private _blinker: TitleBlinkerService) {
    }

    ngOnInit(): void {
        this._game.id = +this._route.snapshot.params.id;
        this.get(this._game.id);
        this._events.connect().pipe(
            switchMap(() => this._service
                .register(this._game.id, this._storage.getConnectionId(this._game.id, this._events.connectionId))
            )
        ).subscribe(value => this.setColor(parseColor(value.color)));

        this._events.onGameUpdated(this._game.id, id => this.get(id));
        this._blinker.captureTitle();
    }

    @OnPageVisibilityChange()
    whenPageVisibilityChanges (): void {
        this.updateAlert();
    }

    private updateAlert(): void {
        if (document.hidden && this._game.activeColor === this._color) {
            this._blinker.startBlink('Your turn!');
        }
        else {
            this._blinker.stopBlink();
        }
    }

    private get(id: number): void {
        if (this._game.id === id) {
            this._service.get(id).subscribe(state => {
                this._game = this._translator.fromApi(state);
                this.config = [state.size, state.corners];
                this.updateAlert();
            });
        }
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

    private setColor(color: Color): void {
        this._color = color;
        this.rotation = this._rotationMap[color];
        console.log(`playing ${this._color}`);
    }

    isSelectable = (x: number, y: number) => {
            const color = this.getPiece(new Point(x, y)).color;
            return color !== Color.NONE && color === this._game.activeColor && color === this._color;
    }

    isOpponent = (piece: Piece) => piece.color !== Color.NONE && piece.color !== this._game.activeColor;

    private getPiece(point: Point): Piece {
        const placement = this._game.pieces.find(value => value.location.x === point.x && value.location.y === point.y);
        return placement && placement.value || new Piece();
    }
}
