import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BoardComponent } from './board/board.component';
import { PieceComponent } from './board/piece/piece.component';
import { AnalysisComponent } from './board/analysis/analysis.component';
import { EvaluatorComponent } from './evaluator/evaluator.component';
import { GameComponent } from './game/game.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ChessService } from './services/chess/chess.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule } from '@angular/forms';
import { TrayComponent } from './evaluator/tray/tray.component';
import { GameTranslationService } from './services/game.translation.service';

const routes: Routes = [
    {path: '', redirectTo: '/game', pathMatch: 'full'},
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
        GameComponent,
        TrayComponent
    ],
    imports: [
        RouterModule.forRoot(routes),
        BrowserModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatCardModule,
        MatSlideToggleModule,
        FormsModule
    ],
    providers: [
        ChessService,
        GameTranslationService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
