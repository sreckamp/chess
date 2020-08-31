'use strict';

// Declare app level module which depends on views, and core components
angular.module('chess', [
  'ngRoute',
  'chess.two',
  'chess.four',
  'chess.version'
]).
config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
  $locationProvider.hashPrefix('!');

  $routeProvider.otherwise({redirectTo: '/two'});
}]);
