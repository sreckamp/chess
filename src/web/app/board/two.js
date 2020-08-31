'use strict';

angular.module('chess.two', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/two/:perspective', {
      templateUrl: 'board/board.html',
      controller: 'TwoPlayerController'
    })
    .when('/two', {
      redirectTo: '/two/white'
    });
}])

.controller('TwoPlayerController', ['$scope', '$routeParams', function($scope, $routeParams) {
  $scope.board = {
    'name': 'Two-Player Game',
    'corners': 0,
    'width': 8,
    'corner': 'light',
    'other': 'dark'
  };
  var pieces = [
    {'piece': 'rook', 'color': 'black'},
    {'piece': 'knight', 'color': 'black'},
    {'piece': 'bishop', 'color': 'black'},
    {'piece': 'queen', 'color': 'black'},
    {'piece': 'king', 'color': 'black'},
    {'piece': 'bishop', 'color': 'black'},
    {'piece': 'knight', 'color': 'black'},
    {'piece': 'rook', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {'piece': 'pawn', 'color': 'black'},
    {},{},{},{},{},{},{},{},
    {},{},{},{},{},{},{},{},
    {},{},{},{},{},{},{},{},
    {},{},{},{},{},{},{},{},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'pawn', 'color': 'white'},
    {'piece': 'rook', 'color': 'white'},
    {'piece': 'knight', 'color': 'white'},
    {'piece': 'bishop', 'color': 'white'},
    {'piece': 'queen', 'color': 'white'},
    {'piece': 'king', 'color': 'white'},
    {'piece': 'bishop', 'color': 'white'},
    {'piece': 'knight', 'color': 'white'},
    {'piece': 'rook', 'color': 'white'},
  ];
  $scope.cornerColor = $scope.board.corner;
  $scope.otherColor = $scope.board.other;
  if ($routeParams.perspective=='black') {
    pieces = pieces.reverse();
  }
  $scope.pieces = pieces;
}]);