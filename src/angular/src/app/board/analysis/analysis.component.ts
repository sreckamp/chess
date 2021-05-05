import { Component, Input, OnInit } from '@angular/core';
import { Rotation } from '../../model/rotation';
import { Color } from '../../model/color';
import { Marker } from '../../model/marker';
import { AnalysisMarker } from './model/analysis.marker';
import { Piece } from '../../model/piece';
import { PieceType } from '../../model/piece.type';

@Component({
    selector: 'app-analysis',
    templateUrl: './analysis.component.html',
    styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
    PieceType = PieceType;
    private _enpassant: Piece = new Piece();
    private _markers: AnalysisMarker[] = [];

    @Input()
    set markers(marks: Marker[]) {
        this._enpassant = new Piece();
        this._markers = marks.reduce((markers, marker) => {
            if (marker.type === 'enpassant') {
                this._enpassant = marker.source;
            } else {
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
                analysis.pieces.push(marker.source);
            }
            return markers;
        }, [] as AnalysisMarker[]);
    }

    get analysisMarkers(): AnalysisMarker[] {
        return this._markers;
    }

    get enpassant(): Piece {
        return this._enpassant;
    }

    @Input()
    color = Color.NONE;

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
}
