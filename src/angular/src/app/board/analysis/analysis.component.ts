import { Component, Input, OnInit } from '@angular/core';
import { Rotation } from '../../model/rotation';
import { Color } from '../../model/color';
import { Marker } from '../../model/marker';
import { AnalysisMarker } from './model/analysis.marker';
import { Piece } from '../../model/piece';
import { PieceType } from '../../model/piece.type';
import { MarkerType } from '../../model/marker.type';

@Component({
    selector: 'app-analysis',
    templateUrl: './analysis.component.html',
    styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
    PieceType = PieceType;
    private _enpassant: Piece = new Piece();
    private _sourceMarkers: Marker[] = [];
    private _markers: AnalysisMarker[] = [];

    @Input()
    set markers(marks: Marker[]) {
        this._sourceMarkers = marks;
        this.updateAnalysisMarkers();
    }

    get analysisMarkers(): AnalysisMarker[] {
        return this._markers;
    }

    get enpassant(): Piece {
        return this._enpassant;
    }

    private _moveColor = Color.NONE;

    @Input()
    set moveColor(value: Color) {
        this._moveColor = value;
        this.updateAnalysisMarkers();
    }

    @Input()
    rotation = Rotation.NONE;

    @Input()
    visible = false;

    constructor() {
    }

    ngOnInit(): void {
    }

    getClasses(marker: AnalysisMarker): string[] {
        let classes = marker.types.map(value => value.toString()).concat(marker.direction);
        if (marker.pieces.length > 1) {
            classes = classes.concat('multiple');
        }
        return classes;
    }

    createTitle(marker: AnalysisMarker): string {
        return marker.types.join('\r\n');
    }

    private updateAnalysisMarkers(): void {
        this._enpassant = new Piece();
        this._markers = this._sourceMarkers.reduce((markers, marker) => {
            if (marker.type === MarkerType.ENPASSANT) {
                this._enpassant = marker.source;
            } else if (marker.type !== MarkerType.MOVE || this._moveColor === marker.source.color) {
                let analysis = markers.find(mark => mark.direction === marker.direction);
                if (!analysis) {
                    analysis = {
                        types: [],
                        pieces: [],
                        direction: marker.direction
                    };
                    markers.push(analysis);
                }
                analysis.types = Array.from(new Set(analysis.types.concat(marker.type)).values());
                analysis.pieces = analysis.pieces.concat([marker.source]
                    .filter(value => !analysis.pieces.some(piece => piece.color === value.color && piece.type === value.type)));
            }
            return markers;
        }, [] as AnalysisMarker[]);
    }
}
