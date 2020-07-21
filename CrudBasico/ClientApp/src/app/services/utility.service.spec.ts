import { TestBed } from '@angular/core/testing';

import { Utility } from './utility.service';

describe('UtilitysService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Utility = TestBed.get(Utility);
    expect(service).toBeTruthy();
  });
});
