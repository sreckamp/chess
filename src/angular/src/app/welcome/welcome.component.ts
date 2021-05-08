import { Component, OnInit } from '@angular/core';
import { ChessService } from '../services/chess/chess.service';
import { GameTranslationService } from '../services/game.translation.service';
import { Router } from '@angular/router';
import { Game } from '../model/game';

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

    games = [] as Game[];

    constructor(private _service: ChessService, private _router: Router) {
    }

    ngOnInit(): void {
    }

    newGame(players: number): void {
        this._service.newGame(players).subscribe(value =>
            this._router.navigate([`/game/${value}`]));
    }

    joinGame(id: number): void {
        this._router.navigate([`/game/${id}`]);
    }
}
