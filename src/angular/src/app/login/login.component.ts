import { Component, OnInit } from '@angular/core';
import { ChessService } from '../services/chess/chess.service';
import { Router } from '@angular/router';
import { GameSummary } from '../services/chess/model/game.summary';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    games = [] as GameSummary[];

    constructor(private _service: ChessService, private _router: Router) {
    }

    ngOnInit(): void {
        this._service.list().subscribe(games => {
            this.games = games;
        });
    }

    newGame(players: number): void {
        this._service.newGame(players).subscribe(value =>
            this._router.navigate([`/game/${value}`]));
    }

    joinGame(id: number): void {
        this._router.navigate([`/game/${id}`]);
    }
}
