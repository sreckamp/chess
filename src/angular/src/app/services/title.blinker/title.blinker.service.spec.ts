import { TestBed } from '@angular/core/testing';
import { Title } from '@angular/platform-browser';
import { TitleBlinkerService } from './title.blinker.service';

describe('EventService', () => {
  let service: TitleBlinkerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
          Title
      ]
    });
    service = TestBed.inject(TitleBlinkerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
