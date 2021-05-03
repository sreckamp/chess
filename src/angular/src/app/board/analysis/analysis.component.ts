import {Component, Input, OnInit} from '@angular/core';
import {Rotation} from '../../model/rotation';
import {Color} from "../../model/color";
import {Marker} from "../../model/marker";
import { PieceType } from 'src/app/model/piece.type';

@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
  PieceType = PieceType;

  @Input()
  markers: Marker[] = [];

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
