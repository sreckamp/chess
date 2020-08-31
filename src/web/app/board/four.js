'use strict';

angular.module('chess.four', ['ngRoute', 'chess.board'])

    .config(['$routeProvider', function($routeProvider) {
      $routeProvider.when('/four/:perspective', {
        templateUrl: 'board/board.html',
        controller: 'BoardController',
        resolve: {
          boardProvider: fourPlayerBoardProvider
        }
      })
          .when('/four', {
            redirectTo: '/four/white'
          });
    }]);

    function fourPlayerBoardProvider() {
      return {
        'name': 'Four-Player Game',
        'corners': 3,
        'width': 14,
        'corner': 'dark',
        'other': 'light',
        'rotationMap': {
          'white': rotations.NONE,
          'silver': rotations.COUNTERCLOCKWISE,
          'black' : rotations.UPSIDEDOWN,
          'gold': rotations.CLOCKWISE
        },
        'pieces': [
          {}, {}, {},
          {'piece': 'rook', 'color': 'black'},
          {'piece': 'knight', 'color': 'black'},
          {'piece': 'bishop', 'color': 'black'},
          {'piece': 'queen', 'color': 'black'},
          {'piece': 'king', 'color': 'black'},
          {'piece': 'bishop', 'color': 'black'},
          {'piece': 'knight', 'color': 'black'},
          {'piece': 'rook', 'color': 'black'},
          {}, {}, {},
          {}, {}, {},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {'piece': 'pawn', 'color': 'black'},
          {}, {}, {},
          {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {},
          {'piece': 'rook', 'color': 'silver'},{'piece': 'pawn', 'color': 'silver'},{}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'rook','color': 'gold'},
          {'piece': 'knight', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'knight','color': 'gold'},
          {'piece': 'bishop', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'bishop','color': 'gold'},
          {'piece': 'king', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'queen','color': 'gold'},
          {'piece': 'queen', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'king','color': 'gold'},
          {'piece': 'bishop', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'bishop','color': 'gold'},
          {'piece': 'knight', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'knight','color': 'gold'},
          {'piece': 'rook', 'color': 'silver'}, {'piece': 'pawn','color': 'silver'}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {'piece': 'pawn', 'color': 'gold'}, {'piece': 'rook','color': 'gold'},
          {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {},
          {}, {}, {},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {'piece': 'pawn', 'color': 'white'},
          {}, {}, {},
          {}, {}, {},
          {'piece': 'rook', 'color': 'white'},
          {'piece': 'knight', 'color': 'white'},
          {'piece': 'bishop', 'color': 'white'},
          {'piece': 'king', 'color': 'white'},
          {'piece': 'queen', 'color': 'white'},
          {'piece': 'bishop', 'color': 'white'},
          {'piece': 'knight', 'color': 'white'},
          {'piece': 'rook', 'color': 'white'},
          {}, {}, {},
        ]
      };
    };
