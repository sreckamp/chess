import {Component, Input, OnInit} from '@angular/core';
import {Rotation} from '../../model/rotation';

@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
  markers = [];

  @Input()
  color = 'none';

  @Input()
  rotation = Rotation.NONE;

  @Input()
  visible = false;

  constructor() { }

  ngOnInit(): void {
  }

}
