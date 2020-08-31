'use strict';

angular.module('chess.four', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
      $routeProvider.when('/four/:perspective', {
        templateUrl: 'board/board.html',
        controller: 'FourPlayerController'
      })
          .when('/four', {
            redirectTo: '/four/white'
          });
    }])

    .controller('FourPlayerController', ['$scope', '$routeParams', function($scope, $routeParams) {
      $scope.board = {
        'name': 'Four-Player Game',
        'corners': 3,
        'width': 14,
        'corner': 'dark',
        'other': 'light'
      };
      var pieces = [
        {},{},{},
        {'piece': 'rook', 'color': 'black'},
        {'piece': 'knight', 'color': 'black'},
        {'piece': 'bishop', 'color': 'black'},
        {'piece': 'queen', 'color': 'black'},
        {'piece': 'king', 'color': 'black'},
        {'piece': 'bishop', 'color': 'black'},
        {'piece': 'knight', 'color': 'black'},
        {'piece': 'rook', 'color': 'black'},
        {},{},{},
        {},{},{},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {'piece': 'pawn', 'color': 'black'},
        {},{},{},
        {},{},{},{},{},{},{},{},{},{},{},{},{},{},
        {'piece': 'rook', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'rook', 'color': 'gold'},
        {'piece': 'knight', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'knight', 'color': 'gold'},
        {'piece': 'bishop', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'bishop', 'color': 'gold'},
        {'piece': 'king', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'queen', 'color': 'gold'},
        {'piece': 'queen', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'king', 'color': 'gold'},
        {'piece': 'bishop', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'bishop', 'color': 'gold'},
        {'piece': 'knight', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'knight', 'color': 'gold'},
        {'piece': 'rook', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{},{},{},{},{},{},{},{},{},{},{'piece': 'pawn', 'color': 'gold'},{'piece': 'rook', 'color': 'gold'},
        {},{},{},{},{},{},{},{},{},{},{},{},{},{},
        {},{},{},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {'piece': 'pawn', 'color': 'white'},
        {},{},{},
        {},{},{},
        {'piece': 'rook', 'color': 'white'},
        {'piece': 'knight', 'color': 'white'},
        {'piece': 'bishop', 'color': 'white'},
        {'piece': 'king', 'color': 'white'},
        {'piece': 'queen', 'color': 'white'},
        {'piece': 'bishop', 'color': 'white'},
        {'piece': 'knight', 'color': 'white'},
        {'piece': 'rook', 'color': 'white'},
        {},{},{},
      ];
      $scope.cornerColor = $scope.board.corner;
      $scope.otherColor = $scope.board.other;
      if ($routeParams.perspective=='black') {
        pieces = rotate(pieces, $scope.board.width, 'upsidedown');

      } else if ($routeParams.perspective=='silver') {
        pieces = rotate(pieces, $scope.board.width, 'counterclockwise');
        $scope.cornerColor = $scope.board.other;
        $scope.otherColor = $scope.board.corner;
      } else if ($routeParams.perspective=='gold') {
        pieces = rotate(pieces, $scope.board.width, 'clockwise');
        $scope.cornerColor = $scope.board.other;
        $scope.otherColor = $scope.board.corner;
      }
      $scope.pieces = pieces;
    }]);

function rotate(array, width, direction) {
  var newArray = [];
  var i, div, mod;
  if(direction == 'clockwise') {
    for (i=0; i < array.length; i++) {
      div = Math.floor(i/width);
      mod = i%width;
      newArray.push(array[width * (width - (mod + 1)) + div]);
    }
  } else if(direction == 'upsidedown') {
    newArray = array.reverse();
  } else if(direction == 'counterclockwise') {
    for (i=0; i < array.length; i++) {
      div = Math.floor(i/width);
      mod = i%width;
      newArray.push(array[(width * (mod + 1)) - (div + 1)]);
    }
  } else {
    newArray = array;
  }
  return newArray;
}