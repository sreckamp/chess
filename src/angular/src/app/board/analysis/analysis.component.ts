import { Component, Input, OnInit } from '@angular/core';
import { Rotation } from '../../model/rotation';
import { Color } from '../../model/color';
import { Marker } from '../../model/marker';

@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
  private _markers: Marker[] = [];

  @Input()
  set markers(marks: Marker[]) {
    this._markers = marks;
  }

  get markers(): Marker[] {
    console.log('markers', this._markers);
    return this._markers;
  }

  @Input()
  color = Color.NONE;

  @Input()
  rotation = Rotation.NONE;

  @Input()
  visible = false;

  constructor() { }

  ngOnInit(): void {
  }

}
