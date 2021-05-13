import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    private _index = 0;
    private readonly _backgroundUrls = [
        'assets/garden.jpg',
        'assets/generational.jpg',
        'assets/glass.jpg',
        'assets/modern.jpg',
        'assets/ocean.jpg',
        'assets/red-glass.jpg',
        'assets/sunrise.jpg',
        'assets/wooden.png',
    ];

    backgroundUrl = 'assets/red-glass.jpg';

    constructor() {
        this.changeBackground();
    }

    changeBackground(): void {
        this.backgroundUrl = this._backgroundUrls[this._index++];
        if (this._index >= this._backgroundUrls.length){
            this._index = 0;
        }
    }
}
