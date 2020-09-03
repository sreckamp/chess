'use strict';

// Declare app level module which depends on views, and core components
angular.module('chess', [
  'ngRoute',
  'ngResource',
  'chess.dynamic',
  'chess.newGame',
  'chess.gameService',
  'chess.version'
])
.config(['$routeProvider', function($routeProvider) {
  $routeProvider.otherwise({redirectTo: '/games'});
}])
    .filter('rotate');
