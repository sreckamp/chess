import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BoardComponent } from './board/board.component';
import { PieceComponent } from './board/piece/piece.component';
import { AnalysisComponent } from './board/analysis/analysis.component';
import { EvaluatorComponent } from './evaluator/evaluator.component';
import { GameComponent } from './game/game.component';
import {RouterModule, Routes} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {ChessService} from './services/chess.service';

const routes: Routes = [
  { path: '', redirectTo: '/game', pathMatch: 'full' },
  {path: 'evaluate', component: EvaluatorComponent},
  {path: 'game', component: GameComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    BoardComponent,
    PieceComponent,
    AnalysisComponent,
    EvaluatorComponent,
    GameComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    HttpClientModule
  ],
  providers: [ChessService],
  bootstrap: [AppComponent]
})
export class AppModule { }
