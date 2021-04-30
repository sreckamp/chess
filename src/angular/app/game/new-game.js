'use strict';

angular.module('chess.newGame', ['ngRoute', 'chess.gameService'])

    .config(['$routeProvider', function($routeProvider) {
        $routeProvider.when('/games', {
            templateUrl: 'game/board/board.html',
            controller: 'NewGameController'
        });
    }])
    .controller('NewGameController', ['gameService', '$routeParams', '$location', function (gameService, $routeParams, $location) {

        var _players = 4;
        if ($routeParams.players) {
            _players = $routeParams.players;
        }

        gameService.newGame(_players).$promise.then(function (board){
            var _path = $location.path();
            $location.url(_path + "/" + board.gameId);
        });
    }
    ]);