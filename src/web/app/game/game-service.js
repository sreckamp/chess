'use strict';

angular.module('chess.gameService', ['ngResource'])
    .factory('gameService', function ($resource) {
        var gameService = {};

        var _newGameResource = $resource('https://localhost:5001/chess/games');
        var _gameResource = $resource('https://localhost:5001/chess/games/:id');
        var _moveResource = $resource('https://localhost:5001/chess/games/:id/moves/:color');

        gameService.newGame = function (players) {
            return _newGameResource.get({players:players});
        };

        gameService.getGame = function(game){
            return _gameResource.get({id:game});
        };

        gameService.getAvailable = function(game, color, x, y){
            return _moveResource.get({id:game, color:color, x:x, y:y});
        };

        gameService.postMove = function(game, color, fromX, fromY, toX, toY){
            return _moveResource.save({id:game, color:color}, {
                'from':{ 'x': fromX, 'y': fromY },
                'to':{ 'x': toX, 'y': toY }
            });
        };

        return gameService;
    });
