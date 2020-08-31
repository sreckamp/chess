'use strict';

angular.module('chess.dynamic', ['ngRoute', 'chess.board', 'ngHttp'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/dynamic/:game/:perspective', {
      templateUrl: 'board/board.html',
      controller: 'BoardController',
      resolve: {
        boardProvider: dynamicBoardSource
      }
    })
    .when('/two', {
      redirectTo: '/two/white'
    });
}]);

function dynamicBoardSource() {
  $http({
    method: 'GET',
    url: ''
  });
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
