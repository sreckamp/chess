import { Piece } from './piece';
import { MarkerType } from './marker.type';
import { Direction } from './direction';

export class Marker {
    type: MarkerType;
    direction: Direction;
    source: Piece;
}
