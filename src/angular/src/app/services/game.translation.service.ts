import { Injectable } from '@angular/core';
import { Game as ApiGame, Marker as ApiMarker, Piece as ApiPiece } from '../services/chess/model/game';
import { Piece } from '../model/piece';
import { Placement } from '../model/placement';
import { Marker } from '../model/marker';
import { Color, parseColor } from '../model/color';
import { parsePieceType } from '../model/piece.type';
import { parseDirection } from '../model/direction';
import { parseMarkerType } from '../model/marker.type';
import { Game } from '../model/game';

@Injectable()
export class GameTranslationService {
    public toApi(game: Game, includeMarkers = false): ApiGame {
        return {
            gameId: game.id,
            name: game.name,
            size: game.size,
            corners: game.corners,
            currentPlayer: game.activeColor,
            moveHistory: null,
            pieces: game.pieces.map(placement => {
                const markers = includeMarkers && game.markers.find(place =>
                    place.location.x === placement.location.x && place.location.y === placement.location.y);
                return {
                    color: placement.value.color.toString(),
                    type: placement.value.type.toString(),
                    location: {
                        x: placement.location.x,
                        y: placement.location.y,
                        metadata: {markers: ((markers && markers.value) || []).map(value => ({
                                direction: value.direction,
                                type: value.type,
                                sourceColor: value.source.color,
                                sourceType: value.source.type
                            }))}
                    }
                } as ApiPiece;
            })
        } as ApiGame;
    }

    public fromApi(game: ApiGame): Game {
        return {
            id: game.gameId,
            size: game.size,
            corners: game.corners,
            name: game.name,
            activeColor: game.currentPlayer && parseColor(game.currentPlayer) || Color.NONE,
            pieces: (game.pieces || []).map(value => {
                const piece = {
                    color: parseColor(value.color),
                    type: parsePieceType(value.type)
                } as Piece;
                return new Placement<Piece>(value.location.x, value.location.y, piece);
            }).filter(value => value.value.color !== Color.NONE),
            markers: (game.pieces || []).map((apiPiece: ApiPiece) => {
                const markers: Marker[] = apiPiece.location.metadata.markers.map((marker: ApiMarker) => {
                    return {
                        direction: parseDirection(marker.direction),
                        type: parseMarkerType(marker.type),
                        source: {type: parsePieceType(marker.sourceType), color: parseColor(marker.sourceColor)} as Piece
                    } as Marker;
                });
                return new Placement<Marker[]>(apiPiece.location.x, apiPiece.location.y, markers);
            }).filter(value => value.value.length > 0)
        };
    }
}
