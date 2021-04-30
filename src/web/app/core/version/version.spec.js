'use strict';

describe('chess.version module', function() {
  beforeEach(module('chess.version'));

  describe('version service', function() {
    it('should return current version', inject(function(version) {
      expect(version).toEqual('0.1');
    }));
  });
});
