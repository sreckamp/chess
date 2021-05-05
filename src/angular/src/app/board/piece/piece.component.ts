import { Component, Input } from '@angular/core';
import { Piece } from '../../model/piece';
import { PieceType } from '../../model/piece.type';

@Component({
    selector: 'app-piece',
    templateUrl: './piece.component.html',
    styleUrls: ['./piece.component.css']
})
export class PieceComponent {
    @Input()
    piece: Piece;

    PieceType = PieceType;

    constructor() {
    }
}
