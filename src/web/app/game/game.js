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

        var _activeSquare = -1;
        var _available = [];

        var _rotation = rotations.NONE;

        var _rotationMap = {
            'white': rotations.NONE,
            'silver': rotations.COUNTERCLOCKWISE,
            'black': rotations.UPSIDEDOWN,
            'gold': rotations.CLOCKWISE,
        };

        var refreshGame = function () {
            gameService.getGame($routeParams.game).$promise.then(function (board) {
                var _pieces = [];
                var i;
                for (i = 0; i < board.width * board.width; i++) {
                    _pieces.push({});
                }

                board.pieces.forEach(function (p) {
                    var idx = p.location.x + p.location.y * board.width;
                    _pieces[idx] = {'piece': p.type.toLowerCase(), 'color': p.color.toLowerCase()};
                });

                $scope.activeSquare = -1;

                _rotation = _rotationMap[$scope.perspective.toLowerCase()].toLowerCase();
                var _sideView = [rotations.COUNTERCLOCKWISE, rotations.CLOCKWISE].includes(_rotation);

                $scope.board = {
                    'name': board.name,
                    'corners': board.corners,
                    'width': board.width,
                    'corner': _sideView ? board.other : board.corner,
                    'other': _sideView ? board.corner : board.other,
                    'pieces': rotate(_pieces, board.width, _rotation)
                };
            });
        };

        var select = function (idx) {
            _activeSquare = idx;
            var _pc = $scope.board.pieces[idx];

            idx = rotateIndex(idx, $scope.board.width, _rotation);

            var _x = idx % $scope.board.width;
            var _y = Math.floor(idx / $scope.board.width);
            var _color = _pc.color;
            $scope.meta = " (" + _color + " " + _pc.piece + ")";

            gameService.getAvailable($routeParams.game, _color, _x, _y).$promise.then(function success(available) {
                _available = [];
                available.forEach(function (p) {
                    var _rot = _rotation;
                    switch (_rot)
                    {
                        case rotations.COUNTERCLOCKWISE:
                            _rot = rotations.CLOCKWISE;
                            break;
                        case rotations.CLOCKWISE:
                            _rot = rotations.COUNTERCLOCKWISE;
                            break;
                    }
                    var idx = rotateIndex(p.to.x + p.to.y * $scope.board.width, $scope.board.width, _rot);
                    _available.push(idx);
                });
            });
        };

        var move = function (fromIdx, toIdx) {
            var _color = $scope.board.pieces[fromIdx].color;

            fromIdx = rotateIndex(fromIdx, $scope.board.width, _rotation);
            toIdx = rotateIndex(toIdx, $scope.board.width, _rotation);

            var _fromX = fromIdx % $scope.board.width;
            var _fromY = Math.floor(fromIdx / $scope.board.width);
            var _toX = toIdx % $scope.board.width;
            var _toY = Math.floor(toIdx / $scope.board.width);

            return gameService.postMove($routeParams.game, _color, _fromX, _fromY, _toX, _toY).$promise;
        };

        refreshGame();

        $scope.isActive = function (index) {
            return index == _activeSquare;
        };

        $scope.isVisible = function (index) {
            if ($scope.board.corners == 0) return true;
            var x = index % $scope.board.width;
            var y = Math.floor(index / $scope.board.width);
            return !((y < $scope.board.corners || y >= $scope.board.width - $scope.board.corners) &&
                (x < $scope.board.corners || x >= $scope.board.width - $scope.board.corners));
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
