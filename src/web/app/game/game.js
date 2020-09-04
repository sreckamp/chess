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

        var _activeSquare = -1;
        var _available = [];

        var _rotation = rotations.NONE;

        var _rotationMap = {
            'white': rotations.NONE,
            'silver': rotations.CLOCKWISE,
            'black': rotations.UPSIDEDOWN,
            'gold': rotations.COUNTERCLOCKWISE,
        };

        $scope.board = {
                'name': '',
                'corners': 0,
                'size': 0,
                'corner': '',
                'other': '',
                'pieces' : []
        };

        var reverse = function(rotation) {
            var _unrotation = rotation;
            switch (_unrotation)
            {
                case rotations.COUNTERCLOCKWISE:
                    _unrotation = rotations.CLOCKWISE;
                    break;
                case rotations.CLOCKWISE:
                    _unrotation = rotations.COUNTERCLOCKWISE;
                    break;
            }
            return _unrotation;
        };

        var parseStore = function(store) {
            _rotation = _rotationMap[$scope.perspective.toLowerCase()].toLowerCase();
            var _sideView = [rotations.COUNTERCLOCKWISE, rotations.CLOCKWISE].includes(_rotation);
            var pcIdx;

            $scope.currentPlayer = store.currentPlayer;

            while($scope.board.pieces.length < store.size * store.size)
            {
                $scope.board.pieces.push({});
            }

            for(pcIdx=0; pcIdx < $scope.board.pieces.length; pcIdx++) {
                var pc = $scope.board.pieces[pcIdx];

                var _found = store.pieces.some(function (p) {
                    var idx = rotateIndex(p.location.x + p.location.y * store.size, store.size, _rotation);
                    if(pcIdx == idx) {
                        if(!pc.piece || pc.piece != p.type.toLowerCase() || !pc.color || pc.color != p.color.toLowerCase())
                        {
                            $scope.board.pieces[pcIdx] = {'piece': p.type.toLowerCase(), 'color': p.color.toLowerCase()};
                        }
                        return true;
                    }
                    return false;
                });

                if(!_found && pc.color) {
                    $scope.board.pieces[pcIdx] = {};
                }
            }

            var actIdx = store.activeLocation.x + store.activeLocation.y * store.size;

            _activeSquare = actIdx < 0 ? -1 : rotateIndex(actIdx, store.size, _rotation);

            $scope.board.name = store.name;
            $scope.board.corners = store.corners;
            $scope.board.size = store.size;
            $scope.board.corner = _sideView ? store.other : store.corner;
            $scope.board.other = _sideView ? store.corner : store.other;

            _available = [];

            store.available.forEach(function (p) {
                _available.push(rotateIndex(p.x + p.y * store.size, store.size, _rotation));
            });
        };

        var refreshGame = function () {
            gameService.getGame($routeParams.game).$promise.then(function (store) {
                parseStore(store);
            });
        };

        var select = function (idx) {
            _activeSquare = idx;
            var _pc = $scope.board.pieces[idx];

            idx = rotateIndex(idx, $scope.board.size, reverse(_rotation));

            var _x = idx % $scope.board.size;
            var _y = Math.floor(idx / $scope.board.size);
            var _color = _pc.color;
            $scope.meta = " (" + _color + " " + _pc.piece + ")";

            gameService.getAvailable($routeParams.game, _color, _x, _y).$promise.then(function success(store) {
                parseStore(store);
            });
        };

        var move = function (fromIdx, toIdx) {
            var _color = $scope.board.pieces[fromIdx].color;

            fromIdx = rotateIndex(fromIdx, $scope.board.size, reverse(_rotation));
            toIdx = rotateIndex(toIdx, $scope.board.size, reverse(_rotation));

            var _fromX = fromIdx % $scope.board.size;
            var _fromY = Math.floor(fromIdx / $scope.board.size);
            var _toX = toIdx % $scope.board.size;
            var _toY = Math.floor(toIdx / $scope.board.size);

            return gameService.postMove($routeParams.game, _color, _fromX, _fromY, _toX, _toY).$promise.then(function success(store) {
                parseStore(store);
            });
        };

        refreshGame();

        $scope.isActive = function (index) {
            return index == _activeSquare;
        };

        $scope.isVisible = function (index) {
            if ($scope.board.corners == 0) return true;
            var x = index % $scope.board.size;
            var y = Math.floor(index / $scope.board.size);
            return !((y < $scope.board.corners || y >= $scope.board.size - $scope.board.corners) &&
                (x < $scope.board.corners || x >= $scope.board.size - $scope.board.corners));
        };

        $scope.isAvailable = function (index) {
            return (_activeSquare >= 0 && _available.includes(index));
        };

        $scope.isOpponent = function (index) {
            if (!$scope.isVisible(index)) return false;
            return _activeSquare >= 0 &&
                $scope.board.pieces[index].color &&
                $scope.board.pieces[index].color != $scope.board.pieces[_activeSquare].color;
        };

        $scope.clickSquare = function (index) {
            if (!$scope.isVisible(index)) return;
            if (_activeSquare == index) {
                _activeSquare = -1;
                _available = [];
                $scope.meta = null;
            } else if (_activeSquare >= 0) {
                var pc = $scope.board.pieces[_activeSquare];
                var target = $scope.board.pieces[index];
                if (target.color == pc.color) {
                    select(index);
                } else {
                    move(_activeSquare, index).then(function success() {
                        _activeSquare = -1;
                        _available = [];
                        $scope.meta = null;
                        refreshGame();
                    });
                    return;
                }
            } else {
                select(index);
            }
        };

        $scope.clickPiece = function () {
            $scope.meta = 'piece';
        };
    }]);

function rotate(array, width, direction) {
    let newArray = array;
    var i;
    newArray = [];
    for (i = 0; i < array.length; i++) {
        newArray.push(array[rotateIndex(i, width, direction)]);
    }
    return newArray;
}

function rotateIndex(idx, width, direction) {
    switch (direction) {
        case rotations.CLOCKWISE:
        case rotations.COUNTERCLOCKWISE:
            var div = Math.floor(idx / width);
            var mod = idx % width;
            if (direction == rotations.CLOCKWISE) {
                return width * (width - (mod + 1)) + div;
            } else {
                return (width * (mod + 1)) - (div + 1);
            }
            break;
        case rotations.UPSIDEDOWN:
            return (width * width) - idx - 1;
        case rotations.NONE:
            return idx;
    }
}
