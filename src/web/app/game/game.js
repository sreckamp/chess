'use strict';

angular.module('chess.game', ['ngRoute', 'ngResource', 'chess.gameService'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/games/:game/:perspective', {
            templateUrl: 'game/board/board.html',
            controller: 'GameController',
        });
    }])

    .controller('GameController', ['gameService', '$scope', '$routeParams', function (gameService, $scope, $routeParams) {

        if ($routeParams.theme) {
            $scope.theme = $routeParams.theme;
        } else {
            $scope.theme = "blue";
        }

        $scope.perspective = $routeParams.perspective;
        $scope.currentPlayer = '';

        var _activeSquare = null;
        var _available = [];

        $scope.rotation = rotations.NONE;

        var _rotationMap = {
            'white': rotations.NONE,
            'silver': rotations.COUNTERCLOCKWISE,
            'black': rotations.UPSIDEDOWN,
            'gold': rotations.CLOCKWISE,
        };

        $scope.board = {
                'name': '',
                'corners': 0,
                'size': 0,
                'files' : [],
                'ranks' : [],
                'pieces' : []
        };

        var getPiece = function(x, y) {
            if($scope.board.pieces[y] && $scope.board.pieces[y].cols[x]){
                return $scope.board.pieces[y].cols[x];
            }
            return null;
        };

        var parseStore = function(store) {
            $scope.rotation = _rotationMap[$scope.perspective.toLowerCase()].toLowerCase();
            var _sideView = [rotations.COUNTERCLOCKWISE, rotations.CLOCKWISE].includes($scope.rotation);
            $scope.currentPlayer = store.currentPlayer;

            var _idx;
            var _pieces = [];
            for(_idx = 0; _idx < store.size; _idx++) {
                if($scope.board.pieces.length != store.size) {
                    $scope.board.files.push(String.fromCharCode("A".charCodeAt(0)+_idx));
                    $scope.board.ranks.push(_idx + 1);
                }
                _pieces.push({'cols': new Array(store.size)});
            }

            $scope.headers = _sideView ? $scope.board.ranks : $scope.board.files;
            $scope.labels = _sideView ? $scope.board.files : $scope.board.ranks;

            store.pieces.forEach(function (p) {
                _pieces[p.location.y].cols[p.location.x] = {'piece': p.type.toLowerCase(), 'color': p.color.toLowerCase()};
            });

            _activeSquare = store.activeLocation.x < 0 ? null : store.activeLocation;

            $scope.board.name = store.name;
            $scope.board.corners = store.corners;
            $scope.board.size = store.size;
            $scope.board.pieces = _pieces;
            $scope.board.corner = store.corners % 2 == 0 ? 'dark' : 'light';
            $scope.board.other = store.corners % 2 == 1 ? 'dark' : 'light';

            _available = [];

            store.available.forEach(function (p) {
                _available.push(p);
            });
        };

        var refreshGame = function () {
            gameService.getGame($routeParams.game).$promise.then(function (store) {
                parseStore(store);
            });
        };

        var select = function (x,y) {
            _activeSquare = {'x':x,'y':y};

            var _pc = getPiece(x, y);

            if(!_pc) return;

            var _color = _pc.color;
            $scope.meta = " (" + _color + " " + _pc.piece + ")";

            gameService.getAvailable($routeParams.game, _color, x, y).$promise.then(function success(store) {
                parseStore(store);
            });
        };

        var move = function (fromX, fromY, toX, toY) {
            var _pc = getPiece(fromX, fromY);

            if(!_pc) return;

            var _color = _pc.color;

            return gameService.postMove($routeParams.game, _color, fromX, fromY, toX, toY).$promise.then(function success(store) {
                parseStore(store);
            });
        };

        refreshGame();

        $scope.isActive = function (x, y) {
            return _activeSquare && x == _activeSquare.x && y == _activeSquare.y;
        };

        $scope.isVisible = function (x, y) {
            if ($scope.board.corners == 0) return true;

            return !((y < $scope.board.corners || y >= $scope.board.size - $scope.board.corners) &&
                (x < $scope.board.corners || x >= $scope.board.size - $scope.board.corners));
        };

        $scope.isAvailable = function (x, y) {
            return _available.some(function (p) {
                return p.x == x && p.y == y;
            });
        };

        $scope.isOpponent = function (x, y) {
            if (!$scope.isVisible(x, y) || !_activeSquare) return false;

            var _pc = getPiece(x, y);
            var _active = getPiece(_activeSquare.x, _activeSquare.y);

            if(!_pc || !_active) return false;

            return _pc.color != $scope.board.pieces[_activeSquare.y].cols[_activeSquare.x].color;
        };

        $scope.clickSquare = function (x,y) {
            if (!$scope.isVisible(x,y)) return;

            var target = getPiece(x, y);

            if (_activeSquare && _activeSquare.x == x && _activeSquare.y == y) {
                _activeSquare = null;
                _available = [];
                $scope.meta = null;
            } else if (_activeSquare != null) {
                var pc = getPiece(_activeSquare.x, _activeSquare.y);

                if(!pc) return;

                if (target && target.color == pc.color) {
                    select(x,y);
                } else {
                    move(_activeSquare.x, _activeSquare.y, x, y).then(function success() {
                        _activeSquare = null;
                        _available = [];
                        $scope.meta = null;
                        refreshGame();
                    });
                    return;
                }
            } else if(target) {
                select(x,y);
            }
        };
    }]);
