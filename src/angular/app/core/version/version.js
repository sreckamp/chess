'use strict';

angular.module('chess.version', [
  'chess.version.interpolate-filter',
  'chess.version.version-directive'
])

.value('version', '0.1');
