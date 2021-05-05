import { Component, OnInit } from '@angular/core';
import { PieceType } from '../model/piece.type';
import { Rotation } from '../model/rotation';
import { Placement, Point } from '../model/placement';
import { Piece } from '../model/piece';
import { Color } from '../model/color';
import { Marker } from '../model/marker';
import { Direction } from '../model/direction';
import { MarkerType } from '../model/marker.type';

@Component({
    selector: 'app-evaluator',
    templateUrl: './evaluator.component.html',
    styleUrls: ['./evaluator.component.css']
})
export class EvaluatorComponent implements OnInit {
    // private readonly _initialPower = [PieceType.ROOK, PieceType.KNIGHT, PieceType.BISHOP, PieceType.QUEEN,
    //   PieceType.KING, PieceType.BISHOP, PieceType.KNIGHT, PieceType.ROOK];

    config: [number, number] = [8, 0];
    rotation = Rotation.NONE;
    rotations = Rotation;
    pieces: Placement<Piece>[] = [{location: {x: 3, y: 3}, value: {color: Color.BLACK, type: PieceType.KING}} as Placement<Piece>];
    markers: Placement<Marker[]>[] = [
        {location: {x: 4, y: 4}, value: [
            {type: MarkerType.ENPASSANT, direction: Direction.NONE, source: {color: Color.WHITE, type: PieceType.PAWN}}
        ]},
        {location: {x: 5, y: 5}, value: [
            {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.WHITE, type: PieceType.KNIGHT}},
            {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.SILVER, type: PieceType.KNIGHT}},
            {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.BLACK, type: PieceType.KNIGHT}},
            {type: MarkerType.COVER, direction: Direction.NONE, source: {color: Color.GOLD, type: PieceType.KNIGHT}}
        ]}
    ];
    rotationKeys = [];
    selected = new Point(-1, -1);
    highlighted = [];

    constructor() {
        this.rotationKeys = Object.keys(this.rotations);
    }

    ngOnInit(): void {
        // this.populate(2);
    }

    changeRotation(rotation: Rotation): void {
        this.rotation = rotation;
    }

    changePlayers(players: number): void {
        this.config = players === 4 ? [14, 3] : [8, 0];
        // this.populate(players);
    }

    // private populate(players: number): void {
    //   const pieces = [];
    //   for (let idx = 0; idx < this._initialPower.length; idx++) {
    //     pieces.push(this.createPlacement(this.config[1] + idx, this.config[0] - 2, Color.BLACK, PieceType.PAWN));
    //     pieces.push(this.createPlacement(this.config[1] + idx, this.config[0] - 1, Color.BLACK, this._initialPower[idx]));
    //     if (players < 4) {
    //       pieces.push(this.createPlacement(this.config[1] + idx, 1, Color.WHITE, PieceType.PAWN));
    //       pieces.push(this.createPlacement(this.config[1] + idx, 0, Color.WHITE, this._initialPower[idx]));
    //     } else {
    //       const reverseIdx = this._initialPower.length - 1 - idx;
    //       pieces.push(this.createPlacement(this.config[1] + idx, 0, Color.WHITE, this._initialPower[reverseIdx]));
    //       pieces.push(this.createPlacement(this.config[1] + idx, 1, Color.WHITE, PieceType.PAWN));
    //       pieces.push(this.createPlacement(0, this.config[1] + idx, Color.SILVER, this._initialPower[idx]));
    //       pieces.push(this.createPlacement(1, this.config[1] + idx, Color.SILVER, PieceType.PAWN));
    //       pieces.push(this.createPlacement(this.config[0] - 1, this.config[1] + idx,
    //         Color.GOLD, this._initialPower[reverseIdx]));
    //       pieces.push(this.createPlacement(this.config[0] - 2, this.config[1] + idx,
    //         Color.GOLD, PieceType.PAWN));
    //     }
    //   }
    //   this.pieces = pieces;
    // }
    // private populate(players: number): void {
    //   const pieces = [];
    //   for (let idx = 0; idx < this._initialPower.length; idx++) {
    //     pieces.push(this.createPlacement(this.config[1] + idx, this.config[0] - 2, Color.BLACK, PieceType.PAWN));
    //     pieces.push(this.createPlacement(this.config[1] + idx, this.config[0] - 1, Color.BLACK, this._initialPower[idx]));
    //     if (players < 4) {
    //       pieces.push(this.createPlacement(this.config[1] + idx, 1, Color.WHITE, PieceType.PAWN));
    //       pieces.push(this.createPlacement(this.config[1] + idx, 0, Color.WHITE, this._initialPower[idx]));
    //     } else {
    //       const reverseIdx = this._initialPower.length - 1 - idx;
    //       pieces.push(this.createPlacement(this.config[1] + idx, 0, Color.WHITE, this._initialPower[reverseIdx]));
    //       pieces.push(this.createPlacement(this.config[1] + idx, 1, Color.WHITE, PieceType.PAWN));
    //       pieces.push(this.createPlacement(0, this.config[1] + idx, Color.SILVER, this._initialPower[idx]));
    //       pieces.push(this.createPlacement(1, this.config[1] + idx, Color.SILVER, PieceType.PAWN));
    //       pieces.push(this.createPlacement(this.config[0] - 1, this.config[1] + idx,
    //         Color.GOLD, this._initialPower[reverseIdx]));
    //       pieces.push(this.createPlacement(this.config[0] - 2, this.config[1] + idx,
    //         Color.GOLD, PieceType.PAWN));
    //     }
    //   }
    //   this.pieces = pieces;
    // }

    // private createPlacement(x: number, y: number, color: Color, type: PieceType): Placement<Piece> {
    //   return new Placement<Piece>(x, y, { color, type } as Piece);
    // }

    clickHandler(point: Point): void {
        this.selected = point;
    }
}
