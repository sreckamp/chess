'use strict';

const rotations = {
    NONE: 'none',
    CLOCKWISE: 'clockwise',
    UPSIDEDOWN: 'upsidedown',
    COUNTERCLOCKWISE: 'counterclockwise'
};

angular.module('chess.board', [])
    .controller('BoardController', ['$scope', '$routeParams', 'boardProvider', function($scope, $routeParams, boardProvider) {
        $scope.theme = "blue";
        if($routeParams.theme) {
            $scope.theme = $routeParams.theme;
        }
        var _board = boardProvider;
        var _activeSquare = -1;

        var _rotation = _board.rotationMap[$routeParams.perspective];
        var _sideView = [rotations.COUNTERCLOCKWISE, rotations.CLOCKWISE].includes(_rotation);

        $scope.activeSquare = -1;

        $scope.board = {
            'name': _board.name,
            'corners': _board.corners,
            'width': _board.width,
            'corner': _sideView ? _board.other : _board.corner,
            'other': _sideView ? _board.corner : _board.other,
            'pieces': rotate(_board.pieces, _board.width, _rotation)
        };

        $scope.isActive = function (index) {
            return index == _activeSquare;
        };

        $scope.isVisible = function (index) {
            if(_board.corners == 0) return true;
            var x = index % _board.width;
            var y = Math.floor(index / _board.width);
            return !(( y < _board.corners || y >= _board.width - _board.corners ) &&
                ( x < _board.corners || x >= _board.width - _board.corners ));
        };

        $scope.isAvailable = function (index) {
            if(_activeSquare < 0 || !$scope.isVisible(index)) return false;
            var actX = _activeSquare % _board.width;
            var actY = Math.floor(_activeSquare / _board.width);
            var x = index % _board.width;
            var y = Math.floor(index / _board.width);
            return Math.abs(x - actX) <= 2 && Math.abs(y - actY) <= 2 ;
            // return !($scope.board.pieces[index].color) || $scope.board.pieces[index].color != $routeParams.perspective;
        };

        $scope.isOpponent = function (index) {
            if(!$scope.isVisible(index)) return false;
            return $scope.board.pieces[index].color && $scope.board.pieces[index].color != $routeParams.perspective;
        };

        $scope.clickSquare = function (index) {
            if(!$scope.isVisible(index)) return;
            if(_activeSquare == index) {
                _activeSquare = -1;
                $scope.meta = null;
            } else if(_activeSquare >= 0) {
                var tmp = $scope.board.pieces[index];
                if(tmp.color == $routeParams.perspective) {
                    $scope.meta = " (" + $scope.board.pieces[index].color + " " + $scope.board.pieces[index].piece + ")";
                    _activeSquare = index;
                } else if(tmp.color) {
                    return;
                } else {
                    $scope.board.pieces[index] = $scope.board.pieces[_activeSquare];
                    $scope.board.pieces[_activeSquare] = tmp;
                    _activeSquare = -1;
                    $scope.meta = null;
                }
            } else {
                if($scope.board.pieces[index].color != $routeParams.perspective) return;

                $scope.meta = " (" + $scope.board.pieces[index].color + " " + $scope.board.pieces[index].piece + ")";
                _activeSquare = index;
            }
        };

        $scope.clickPiece = function () {
            $scope.meta = 'piece';
        };
    }]);

function rotate(array, width, direction) {
    let newArray = array;
    switch (direction)
    {
        case rotations.CLOCKWISE:
        case rotations.COUNTERCLOCKWISE:
            var i, div, mod;
            newArray = [];
            for (i=0; i < array.length; i++) {
                div = Math.floor(i/width);
                mod = i%width;
                if(direction == rotations.CLOCKWISE) {
                    newArray.push(array[width * (width - (mod + 1)) + div]);
                } else {
                    newArray.push(array[(width * (mod + 1)) - (div + 1)]);
                }
            }
            break;
        case rotations.UPSIDEDOWN:
            newArray = array.reverse();
            break;
    }
    return newArray;
}
