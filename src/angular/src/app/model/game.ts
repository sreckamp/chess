import { Color } from './color';
import { Placement } from './placement';
import { Piece } from './piece';
import { Marker } from './marker';

export interface Game {
    id: number;
    name: string;
    size: number;
    corners: number;
    activeColor: Color;
    pieces: Placement<Piece>[];
    markers: Placement<Marker[]>[];
}
