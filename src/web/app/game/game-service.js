'use strict';

angular.module('chess.gameService', ['ngResource'])
    .factory('gameService', function ($resource) {
        var gameService = {};

        var _newGameResource = $resource('https://localhost:5001/chess/games');
        var _gameResource = $resource('https://localhost:5001/chess/games/:id');
        var _moveResource = $resource('https://localhost:5001/chess/games/:id/moves');

        gameService.newGame = function (players) {
            return _newGameResource.get({players:players});
        };

        gameService.getGame = function(game){
            return _gameResource.get({id:game});
        };

        gameService.getAvailable = function(game, x, y){
            return _moveResource.query({id:game, x:x, y:y});
        };

        gameService.postMove = function(game, fromX, fromY, toX, toY){
            return _moveResource.save({id:game}, {
                'from':{ 'x': fromX, 'y': fromY },
                'to':{ 'x': toX, 'y': toY }
            });
        };

        return gameService;
    });
