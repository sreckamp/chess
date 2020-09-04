using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameBase.Model;

namespace Chess.Model
{
    public enum ChessVersion
    {
        TwoPlayer = 2,
        FourPlayer = 4,
    }
    public class Game
    {
        private static readonly Dictionary<ChessVersion, Dictionary<Color, Direction>> Teams =
            new Dictionary<ChessVersion, Dictionary<Color, Direction>>
            {
                {
                    ChessVersion.TwoPlayer, new Dictionary<Color, Direction>
                    {
                        { Color.White, Direction.South },
                        { Color.Black, Direction.North },
                    }
                },
                {
                    ChessVersion.FourPlayer, new Dictionary<Color, Direction>
                    {
                        { Color.White, Direction.South },
                        { Color.Black, Direction.North },
                        { Color.Silver, Direction.West },
                        { Color.Gold, Direction.East },
                    }
                }
            };

        public ChessVersion Version { get; }

        public Game(ChessVersion version)
        {
            Version = version;
            Board = new Board(version == ChessVersion.TwoPlayer ? 0 : 3);
        }

        public Board Board { get; }

        public void Init()
        {
            foreach(var team in Teams[Version])
            {
                PopulateSide(team.Key, team.Value, Board);
            }
        }

        private void Place(Piece piece, int x, int y)
        {
            var p = new Placement<Piece>(piece, new Point(x, y));
            Board.Add(p);
        }

        private void PopulateSide(Color color, Direction dir, Board board)
        {
            var kingOnLeft = board.CornerSize > 0;
            var vert = true;
            var power = 0;
            var pawn = 1;
            var kingFile = 4;

            switch (dir)
            {
                case Direction.South:
                    power = board.Height - 1;
                    pawn = board.Height - 2;
                    kingFile = kingOnLeft ? 3 : kingFile;
                    break;
                case Direction.West:
                    vert = false;
                    kingFile = kingOnLeft ? 3 : kingFile;
                    break;
                case Direction.North:
                    break;
                case Direction.East:
                    power = board.Width - 1;
                    pawn = board.Width - 2;
                    vert = false;
                    break;
            }

            for (var idx = 0; idx < 8; idx++)
            {
                Piece p = null;
                switch (idx)
                {
                    case 0:
                    case 7:
                        p = Piece.CreateRook(color);
                        break;
                    case 1:
                    case 6:
                        p = Piece.CreateKnight(color);
                        break;
                    case 2:
                    case 5:
                        p = Piece.CreateBishop(color);
                        break;
                    default:
                        p = idx == kingFile
                            ? Piece.CreateKing(color)
                            : Piece.CreateQueen(color);
                        break;
                }

                Place(p, vert ? idx + board.CornerSize : power, vert ? power : idx + board.CornerSize);
                Place(Piece.CreatePawn(color, dir.Opposite()), vert ? idx + board.CornerSize : pawn, vert ? pawn : idx + board.CornerSize);
            }
        }

        public IEnumerable<Point> GetPossibleMoves(Color color, Point point)
        {
            //TODO: Verify current player
            return Board.GetPossibleMoves(color, point);
        }

        public bool Move(Color color, Point from, Point to)
        {
            if (!GetPossibleMoves(color, @from).Contains(to)) return false;

            //TODO: Verify current player
            var taken = Board.Move(@from, to);
            //TODO: Write this somewhere
            //TODO: Write to move list
            //TODO: Move to next player
            return true;
        }
    }
}