'use strict';

// Declare app level module which depends on views, and core components
angular.module('chess', [
  'ngRoute',
  'ngResource',
  'chess.two',
  'chess.four',
  'chess.dynamic',
  'chess.version'
])
.config(['$routeProvider', function($routeProvider) {
  $routeProvider.otherwise({redirectTo: '/two'});
}])
    .filter('rotate');
