import { Piece } from '../../../model/piece';
import { Direction } from '../../../model/direction';
import { MarkerType } from '../../../model/marker.type';

export interface AnalysisMarker {
    direction: Direction;
    types: MarkerType[];
    pieces: Piece[];
}
