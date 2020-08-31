'use strict';

// Declare app level module which depends on views, and core components
angular.module('chess', [
  'ngRoute',
  'chess.two',
  'chess.four',
  'chess.version'
])
.config(['$routeProvider', function($routeProvider) {
  $routeProvider.otherwise({redirectTo: '/two'});
}])
    .filter('rotate');
