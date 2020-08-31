'use strict';

angular.module('chess.two', ['ngRoute', 'chess.board'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/two/:perspective', {
      templateUrl: 'board/board.html',
      controller: 'BoardController',
      resolve: {
        boardProvider: twoPlayerBoardSource
      }
    })
    .when('/two', {
      redirectTo: '/two/white'
    });
}]);

function twoPlayerBoardSource() {
  return {
    'name': 'Two-Player Game',
    'corners': 0,
    'width': 8,
    'corner': 'light',
    'other': 'dark',
    'rotationMap': {
      'white': rotations.NONE,
      'black': rotations.UPSIDEDOWN,
    },
    'pieces': [
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
      {}, {}, {}, {}, {}, {}, {}, {},
      {}, {}, {}, {}, {}, {}, {}, {},
      {}, {}, {}, {}, {}, {}, {}, {},
      {}, {}, {}, {}, {}, {}, {}, {},
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
    ]
  };
}
