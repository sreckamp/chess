using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model
{
    public class Piece
    {
        private static readonly Direction[] s_all = (Direction[])Enum.GetValues(typeof(Direction));
        private static readonly Direction[] s_diagonals = {
            Direction.NorthEast, Direction.SouthEast,
            Direction.SouthWest, Direction.NorthWest
        };
        private static readonly Direction[] s_cardinals = {
            Direction.North, Direction.East,
            Direction.South, Direction.West
        };
        private readonly Dictionary<Direction, IMovementRule> m_moveRules = new Dictionary<Direction, IMovementRule>();
        private Dictionary<Direction, IMovementRule> m_attackRules;
        private Piece(string name, string team)
        {
            Name = name;
            Team = team;
            m_attackRules = m_moveRules;
            OptionalFirstRule = StraightMovementRule.None;
        }

        public string Name { get; }
        public string Team { get; }

        public IMovementRule OptionalFirstRule { get; private set; }

        public Dictionary<Direction, IMovementRule> MoveRules => m_moveRules;

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Team))
            {
                sb.Append((Team)).Append(' ');
            }
            return sb.Append(Name).ToString();
        }

        public static Piece CreateKing(string team)
        {
            var king = new Piece("King", team);
            foreach (var dir in s_all)
            {
                king.m_moveRules[dir] = StraightMovementRule.OneSpace;
            }
            //TODO: Castling

            return king;
        }

        public static Piece CreateQueen(string team)
        {
            var queen = new Piece("Queen", team);
            foreach (var dir in s_all)
            {
                queen.m_moveRules[dir] = StraightMovementRule.Infinite;
            }

            return queen;
        }

        public static Piece CreateBishop(string team)
        {
            var bishop = new Piece("Bishop", team);
            foreach (var dir in s_all)
            {
                bishop.m_moveRules[dir] = s_diagonals.Contains(dir)
                    ? StraightMovementRule.Infinite
                    : StraightMovementRule.None;
            }

            return bishop;
        }

        public static Piece CreateRook(string team)
        {
            var rook = new Piece("Rook", team);
            foreach (var dir in s_all)
            {
                rook.m_moveRules[dir] = s_cardinals.Contains(dir)
                    ? StraightMovementRule.Infinite
                    : StraightMovementRule.None;
            }

            return rook;
        }

        public static Piece CreateKnight(string team)
        {
            var knight = new Piece("Knight", team);
            foreach (var dir in s_all)
            {
                knight.m_moveRules[dir] = KnightMovementRule.Instance;
            }

            return knight;
        }

        public static Piece CreatePawn(string team, Direction dir)
        {
            var pawn = new Piece("Pawn", team);
            pawn.m_moveRules[dir] = StraightMovementRule.OneSpace;
            pawn.OptionalFirstRule = StraightMovementRule.TwoSpace;
            pawn.m_attackRules = new Dictionary<Direction, IMovementRule>();
            var attacks = new []
            {
                (Direction)(((int)dir - 1) % s_all.Length),
                (Direction)(((int)dir + 1) % s_all.Length),
            };
            foreach (var d in s_all)
            {
                pawn.m_attackRules[dir] = attacks.Contains(d)
                    ? StraightMovementRule.OneSpace
                    : StraightMovementRule.None;
            }

            return pawn;
        }

        public static Piece CreateEmpty()
        {
            var blank = new Piece("blank", "");
            foreach (var d in s_all)
            {
                blank.m_moveRules[d] = StraightMovementRule.None;
            }

            return blank;
        }
    }
}
