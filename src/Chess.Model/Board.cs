using System;
using System.Data.Common;
using GameBase.Model;

namespace Chess.Model
{
    public class Board : GameBoard<Piece, Move>
    {
        private const int ROWS = 8;
        private const int COLUMNS = 8;

        public Board(int cornerSize = 0) : base(null, COLUMNS + 2 * cornerSize, ROWS + 2 * cornerSize)
        {
            CornerSize = cornerSize;
        }

        public int Height => ROWS + 2 * CornerSize;
        public int Width => COLUMNS + 2 * CornerSize;

        public int CornerSize { get; }

        /// <inheritdoc />
        public override void Clear()
        {
            base.Clear();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var isXCorner = x < CornerSize || x >= Height - CornerSize;
                    var isYCorner = y < CornerSize || y >= Width - CornerSize;
                    if(isXCorner && isYCorner) continue;
                    var m = new Move(x, y);
                    var p = new Placement<Piece,Move>(Piece.CreateEmpty(), m);
                    Add(p, false);
                }
            }
        }

        protected override void AddAvailableLocations(Placement<Piece, Move> p)
        {
            if("blank".Equals(p?.Piece?.Name))
            {
                AvailableLocations.Add(p.Move.Location);
            }
        }

    }
}