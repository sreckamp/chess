import { TestBed } from '@angular/core/testing';

import { BlockedStorageService } from './blocked.storage.service';

describe('StorageService', () => {
  let service: BlockedStorageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlockedStorageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
