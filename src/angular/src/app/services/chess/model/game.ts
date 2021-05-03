export interface Marker {
  type: string;
  sourceType: string;
  sourceColor: string;
  direction: string;
}

export interface Square {
  x: number;
  y: number;
  metadata: Marker[];
}

export interface Piece {
  color: string;
  type: string;
  location: Square;
}

export interface Game {
  gameId: number;
  size: number;
  corners: number;
  currentPlayer: string;
  moveHistory: any[];
  name: string;
  pieces: Piece[];
}
