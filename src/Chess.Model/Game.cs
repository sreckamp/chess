using System.Collections.Generic;
using System.Drawing;
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
        private static readonly Dictionary<ChessVersion, Dictionary<string, Direction>> TEAMS =
            new Dictionary<ChessVersion, Dictionary<string, Direction>>
            {
                {
                    ChessVersion.TwoPlayer, new Dictionary<string, Direction>
                    {
                        { "White", Direction.South },
                        { "Black", Direction.North },
                    }
                },
                {
                    ChessVersion.FourPlayer, new Dictionary<string, Direction>
                    {
                        { "White", Direction.South },
                        { "Black", Direction.North },
                        { "Silver", Direction.West },
                        { "Gold", Direction.East },
                    }
                }
            };

        private readonly ChessVersion m_version;

        public Game(ChessVersion version)
        {
            m_version = version;
            Board = new Board(version == ChessVersion.TwoPlayer ? 0 : 3);
        }

        public Board Board { get; }

        public void Init()
        {
            foreach (var team in TEAMS[m_version])
            {
                populateSide(team.Key, team.Value, Board);
            }
        }

        private void Place(Piece piece, int x, int y)
        {
            var p = new Placement<Piece>(piece, new Point(x, y));
            Board.Add(p);
        }

        public void Play()
        {
            Board.Clear();
            Init();
            while(true);
        }

        private void populateSide(string team, Direction dir, Board board)
        {
            var kingOnLeft = board.CornerSize > 0;
            var vert = true;
            var power = 0;
            var pawn = 1;
            var kingFile = 4;

            switch (dir)
            {
                case Direction.North:
                    power = board.Height - 1;
                    pawn = board.Height - 2;
                    kingFile = kingOnLeft ? 3 : kingFile;
                    break;
                case Direction.West:
                    vert = false;
                    kingFile = kingOnLeft ? 3 : kingFile;
                    break;
                case Direction.South:
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
                        p = Piece.CreateRook(team);
                        break;
                    case 1:
                    case 6:
                        p = Piece.CreateKnight(team);
                        break;
                    case 2:
                    case 5:
                        p = Piece.CreateBishop(team);
                        break;
                    default:
                        p = idx == kingFile
                            ? Piece.CreateKing(team)
                            : Piece.CreateQueen(team);
                        break;
                }

                Place(p, vert ? idx + board.CornerSize : power, vert ? power : idx + board.CornerSize);
                Place(Piece.CreatePawn(team, dir), vert ? idx + board.CornerSize : pawn, vert ? pawn : idx + board.CornerSize);
            }
        }
    }
}