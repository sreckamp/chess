import { Component, HostBinding, Input } from '@angular/core';
import { Piece } from '../../model/piece';
import { PieceType } from '../../model/piece.type';

@Component({
    selector: 'app-piece',
    templateUrl: './piece.component.html',
    styleUrls: ['./piece.component.css']
})
export class PieceComponent {
    @Input()
    piece = new Piece();

    @Input()
    phantom = false;

    @HostBinding('class')
    get classes(): string {
        return `${this.piece.color} ${this.piece.type}` + (this.phantom ? ' phantom' : '');
    }

    PieceType = PieceType;

    constructor() {
    }
}
