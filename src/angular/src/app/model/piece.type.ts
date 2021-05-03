export enum PieceType {
  EMPTY = 'empty',
  PAWN = 'pawn',
  KNIGHT = 'knight',
  BISHOP = 'bishop',
  ROOK = 'rook',
  QUEEN = 'queen',
  KING = 'king'
}

export function parsePieceType(value: string): PieceType {
  return value && PieceType[value.toUpperCase()] || PieceType.EMPTY;
}
