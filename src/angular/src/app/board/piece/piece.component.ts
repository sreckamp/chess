import {Component, Input, OnInit} from '@angular/core';
import {colors} from '@angular/cli/utilities/color';
import {PieceType} from '../../model/piece.type';
import {Color} from '../../model/color';

@Component({
  selector: 'app-piece',
  templateUrl: './piece.component.html',
  styleUrls: ['./piece.component.css']
})
export class PieceComponent implements OnInit {
  @Input()
  color = Color.NONE;

  @Input()
  piece = PieceType.EMPTY;

  constructor() { }

  ngOnInit(): void {
  }

}
