'use strict';

angular.module('chess.game', ['ngRoute', 'ngResource', 'chess.gameService'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/games/:game', {
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

        if($routeParams.meta) {
            $scope.showMeta = true;
        }

        let _bottom = 'white';

        if($routeParams.bottom) {
            _bottom = $routeParams.bottom;
        }

        const _rotationMap = {
            'white': rotations.NONE,
            'silver': rotations.COUNTERCLOCKWISE,
            'black': rotations.UPSIDEDOWN,
            'gold': rotations.CLOCKWISE,
        };

        $scope.rotation = _rotationMap[_bottom.toLowerCase()];

        $scope.currentPlayer = '';

        let _activeSquare = null;
        let _available = [];

        $scope.board = {
                'name': '',
                'corners': 0,
                'size': 0,
                'files' : [],
                'ranks' : [],
                'pieces' : []
        };

        const getPiece = function(x, y) {
            if($scope.board.pieces[y] && $scope.board.pieces[y].cols[x]){
                return $scope.board.pieces[y].cols[x];
            }
            return null;
        };

        const parseStore = function(store) {
            const _sideView = [rotations.COUNTERCLOCKWISE, rotations.CLOCKWISE].includes($scope.rotation);
            $scope.currentPlayer = store.currentPlayer;

            let _idx;
            const _pieces = [];
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
                const _enpassant = p.location.metadata.markers.find(value => value.type == 'enpassant');
                _pieces[p.location.y].cols[p.location.x] = {
                    'piece': _enpassant ? _enpassant.type : p.type,
                    'color': _enpassant ? _enpassant.sourceColor : p.color,
                    'metadata': {
                        'markers': p.location.metadata.markers.reduce(function (arr, m) {
                            if(m.type != 'enpassant') {
                                _idx = arr.findIndex(item => item.direction === m.direction);
                                if (_idx < 0) {
                                    arr.push({
                                        'direction': m.direction,
                                        'types': [],
                                        'pieces': []
                                    });
                                    _idx = arr.findIndex(item => item.direction === m.direction);
                                }
                                arr[_idx].types.push(m.type);
                                arr[_idx].pieces.push({
                                    'color': m.sourceColor,
                                    'piece': m.sourceType,
                                });
                            }
                            return arr;
                        }, [])
                    }
                };
            });

            $scope.board.name = store.name;
            $scope.board.corners = store.corners;
            $scope.board.size = store.size;
            $scope.board.pieces = _pieces;
            $scope.board.corner = store.corners % 2 == 0 ? 'dark' : 'light';
            $scope.board.other = store.corners % 2 == 1 ? 'dark' : 'light';
        };

        const refreshGame = function () {
            gameService.getGame($routeParams.game).$promise.then(function (store) {
                parseStore(store);
            });
        };

        const select = function (x,y) {
            const _pc = getPiece(x, y);

            if(!_pc || _pc.color != $scope.currentPlayer.toLowerCase()) return;

            _activeSquare = {'x':x,'y':y};

            gameService.getAvailable($routeParams.game, x, y).$promise.then(function success(available) {
                _available = [];

                available.forEach(function (p) {
                    _available.push(p);
                });
            });
        };

        const move = function (fromX, fromY, toX, toY) {
            const _pc = getPiece(fromX, fromY);

            if(!_pc) return;

            return gameService.postMove($routeParams.game, fromX, fromY, toX, toY).$promise.then(function success(store) {
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

        $scope.isOpponent = function (color) {
            if(color =='none' || !_activeSquare) return false;

            return color != $scope.board.pieces[_activeSquare.y].cols[_activeSquare.x].color;
        };

        $scope.isInCheck = function (x, y) {
            const _pc = getPiece(x, y);

            return _pc && _pc.piece == 'king' && _pc.metadata.markers.some(marker => marker.types.includes('check'));
        };

        $scope.clickSquare = function (x,y) {
            if (!$scope.isVisible(x,y)) return;

            const target = getPiece(x, y);

            if (_activeSquare && _activeSquare.x == x && _activeSquare.y == y) {
                _activeSquare = null;
                _available = [];
            } else if (_activeSquare != null) {
                const pc = getPiece(_activeSquare.x, _activeSquare.y);

                if(!pc) return;

                if (target && (target.color == pc.color && target.color != 'none')) {
                    select(x,y);
                } else {
                    move(_activeSquare.x, _activeSquare.y, x, y).then(function success() {
                        _activeSquare = null;
                        _available = [];
                        refreshGame();
                    });
                    return;
                }
            } else if(target && target.color != 'none') {
                select(x,y);
            }
        };
    }]);
