'use strict';

angular.module('chess.dynamic', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/dynamic/:game/:perspective', {
      templateUrl: 'board/board.html',
      controller: 'DynamicBoardController',
    });
}])
    .controller('DynamicBoardController', ['$http', '$scope', '$routeParams', function ($http, $scope, $routeParams) {
      $scope.theme = "blue";
      $scope.available = [];
      if($routeParams.theme) {
        $scope.theme = $routeParams.theme;
      }
      $scope.perspective = $routeParams.perspective;
  $http.get('https://localhost:5001/chess/game/' + $routeParams.game).then(function success(response) {
    var _temp = response.data;
    var _pieces = [];
    var i;
    for (i = 0; i < _temp.width * _temp.width; i++) {
      _pieces.push({});
    }
    _temp.pieces.forEach(function (p) {
      var idx = p.x + p.y * _temp.width;
      _pieces[idx] = {'piece': p.type.toLowerCase(), 'color': p.color.toLowerCase()};
    });

    var _rotation = _temp.rotationMap[$scope.perspective.toLowerCase()].toLowerCase();
    var _sideView = [rotations.COUNTERCLOCKWISE, rotations.CLOCKWISE].includes(_rotation);

    $scope.activeSquare = -1;

    $scope.board = {
      'name': _temp.name,
      'corners': _temp.corners,
      'width': _temp.width,
      'corner': _sideView ? _temp.other : _temp.corner,
      'other': _sideView ? _temp.corner : _temp.other,
      'pieces': rotate(_pieces, _temp.width, _rotation)
    };
  }, function (response) {
    var tmp =response;
  });

    var _activeSquare = -1;

    $scope.isActive = function (index) {
      return index == _activeSquare;
    };

    $scope.isVisible = function (index) {
      if($scope.board.corners == 0) return true;
      var x = index % $scope.board.width;
      var y = Math.floor(index / $scope.board.width);
      return !(( y < $scope.board.corners || y >= $scope.board.width - $scope.board.corners ) &&
          ( x < $scope.board.corners || x >= $scope.board.width - $scope.board.corners ));
    };

    $scope.isAvailable = function (index) {
      return (_activeSquare >= 0 && $scope.available.includes(index));
    };

    $scope.isOpponent = function (index) {
      if(!$scope.isVisible(index)) return false;
      return $scope.board.pieces[index].color && $scope.board.pieces[index].color != $scope.perspective;
    };

    $scope.clickSquare = function (index) {
      if(!$scope.isVisible(index)) return;
      if(_activeSquare == index) {
        _activeSquare = -1;
        $scope.meta = null;
      } else if(_activeSquare >= 0) {
        var tmp = $scope.board.pieces[index];
        if(tmp.color == $scope.perspective) {
          $scope.meta = " (" + $scope.board.pieces[index].color + " " + $scope.board.pieces[index].piece + ")";
          _activeSquare = index;
          getAvailable($scope, $http, $routeParams.game, index, $scope.board.width);
        } else if(tmp.color) {
          return;
        } else {
          // $scope.board.pieces[index] = $scope.board.pieces[_activeSquare];
          // $scope.board.pieces[_activeSquare] = tmp;
          _activeSquare = -1;
          $scope.meta = null;
        }
      } else {
        if($scope.board.pieces[index].color != $scope.perspective) return;

        $scope.meta = " (" + $scope.board.pieces[index].color + " " + $scope.board.pieces[index].piece + ")";
        _activeSquare = index;
        getAvailable($scope, $http, $routeParams.game, index, $scope.board.width);
      }
    };

    $scope.clickPiece = function () {
      $scope.meta = 'piece';
    };
  }]);

function getAvailable($scope, $http, game, idx, width) {
  $http.get('https://localhost:5001/chess/game/' + game + '/moves?x=' + (idx % width) + '&y=' + (Math.floor(idx / width))).then(function success(response) {
    var _temp = response.data;
    var _available = [];
    _temp.forEach(function (p) {
      _available.push(p.x + p.y * width);
    });
    $scope.available = _available;
  });
}

function rotate(array, width, direction) {
  let newArray = array;
  var i;
  newArray = [];
  for (i=0; i < array.length; i++) {
    newArray.push(array[rotateIndex(i, width, direction)]);
  }
  return newArray;
}

function rotateIndex(idx, width, direction) {
  switch (direction)
  {
    case rotations.CLOCKWISE:
    case rotations.COUNTERCLOCKWISE:
      var div = Math.floor(idx/width);
      var mod = idx % width;
      if(direction == rotations.CLOCKWISE) {
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
