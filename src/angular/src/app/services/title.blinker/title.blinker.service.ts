import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable()
export class TitleBlinkerService {
    private timeout;
    private _startingTitle: string;
    // https://medium.com/@amcdnl/make-tabs-blink-in-angular-7dbd3141e8d

    constructor(private _titleService: Title) {
    }

    public captureTitle() {
        this._startingTitle = this._titleService.getTitle();
    }

    public startBlink(msg1: string, msg2: string = null, placeholder: string = '-'): void {
        msg2 = msg2 || new Array(msg1.length + 1).join( placeholder );
        const step = () => {
            const newTitle = this._titleService.getTitle() === msg1 ? msg2 : msg1;

            this._titleService.setTitle(newTitle);

            this.timeout = setTimeout(step.bind(this), 250);
        }

        clearTimeout(this.timeout);
        step();
    }

    public stopBlink(): void {
        clearTimeout(this.timeout);
        this._titleService.setTitle(this._startingTitle);
    }
}
