import { Piece } from '../../../model/piece';

export interface AnalysisMarker {
    direction: string;
    types: AnalysisMarkerType[];
}

export interface AnalysisMarkerType {
    type: string;
    pieces: Piece[];
}
