using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model
{
    public class Piece
    {
        private static readonly Direction[] SAll = (Direction[])Enum.GetValues(typeof(Direction));
        private static readonly Direction[] SDiagonals = {
            Direction.NorthEast, Direction.SouthEast,
            Direction.SouthWest, Direction.NorthWest
        };
        private static readonly Direction[] SCardinals = {
            Direction.North, Direction.East,
            Direction.South, Direction.West
        };
        private readonly Dictionary<Direction, IMovementRule> m_moveRules = new Dictionary<Direction, IMovementRule>();
        private Dictionary<Direction, IMovementRule> m_firstMoveRules;
        private bool m_hasMoved;

        private Piece(string name, string team)
        {
            Name = name;
            Team = team;
            AttackRules = m_moveRules;
            m_firstMoveRules = m_moveRules;
            IsEmpty = false;
        }

        public string Name { get; }
        public string Team { get; }

        public bool IsEmpty { get; private set; }

        public void Move() { m_hasMoved = true; }

        public Dictionary<Direction, IMovementRule> AttackRules { get; private set; }

        public Dictionary<Direction, IMovementRule> MoveRules => m_hasMoved ? m_moveRules : m_firstMoveRules;

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
            foreach (var dir in SAll)
            {
                king.m_moveRules[dir] = StraightMovementRule.OneSpace;
            }
            //TODO: Castling

            return king;
        }

        public static Piece CreateQueen(string team)
        {
            var queen = new Piece("Queen", team);
            foreach (var dir in SAll)
            {
                queen.m_moveRules[dir] = StraightMovementRule.Infinite;
            }

            return queen;
        }

        public static Piece CreateBishop(string team)
        {
            var bishop = new Piece("Bishop", team);
            foreach (var dir in SAll)
            {
                bishop.m_moveRules[dir] = SDiagonals.Contains(dir)
                    ? StraightMovementRule.Infinite
                    : StraightMovementRule.None;
            }

            return bishop;
        }

        public static Piece CreateRook(string team)
        {
            var rook = new Piece("Rook", team);
            foreach (var dir in SAll)
            {
                rook.m_moveRules[dir] = SCardinals.Contains(dir)
                    ? StraightMovementRule.Infinite
                    : StraightMovementRule.None;
            }

            return rook;
        }

        public static Piece CreateKnight(string team)
        {
            var knight = new Piece("Knight", team);
            foreach (var dir in SAll)
            {
                knight.m_moveRules[dir] = KnightMovementRule.Instance;
            }

            return knight;
        }

        public static Piece CreatePawn(string team, Direction dir)
        {
            dir = (Direction) (((int) dir + SAll.Length / 2) % SAll.Length);

            var pawn = new Piece("Pawn", team)
            {
                AttackRules = new Dictionary<Direction, IMovementRule>(),
                m_firstMoveRules = new Dictionary<Direction, IMovementRule>()
            };

            var attacks = new []
            {
                (Direction)(((int)dir - 1) % SAll.Length),
                (Direction)(((int)dir + 1) % SAll.Length),
            };

            foreach (var d in SAll)
            {
                pawn.m_moveRules[d] = d == dir 
                    ? StraightMovementRule.OneSpace
                    : StraightMovementRule.None;
                pawn.m_firstMoveRules[d] = d == dir
                    ?  StraightMovementRule.TwoSpace
                    : StraightMovementRule.None;
                pawn.AttackRules[d] = attacks.Contains(d)
                    ? StraightMovementRule.OneSpace
                    : StraightMovementRule.None;
            }

            return pawn;
        }

        public static Piece CreateEmpty()
        {
            var blank = new Piece("blank", "") {IsEmpty = true};
            foreach (var d in SAll)
            {
                blank.m_moveRules[d] = StraightMovementRule.None;
            }

            return blank;
        }
    }
}
