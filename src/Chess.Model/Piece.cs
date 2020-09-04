using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model
{
    public class Piece
    {
        private readonly Dictionary<Direction, IMovementRule> m_moveRules = new Dictionary<Direction, IMovementRule>();
        private Dictionary<Direction, IMovementRule> m_firstMoveRules;
        private bool m_hasMoved;

        private Piece(PieceType type, Color color)
        {
            Type = type;
            Color = color;
            AttackRules = m_moveRules;
            m_firstMoveRules = m_moveRules;
        }

        public PieceType Type { get; }

        public Color Color { get; }

        public bool IsEmpty => Type == PieceType.Empty;

        public void Moved() { m_hasMoved = true; }

        public Dictionary<Direction, IMovementRule> AttackRules { get; private set; }

        public Dictionary<Direction, IMovementRule> MoveRules => m_hasMoved ? m_moveRules : m_firstMoveRules;

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Color != Color.None)
            {
                sb.Append($"{Color} ");
            }

            return sb.Append("${Type}").ToString();
        }

        public static Piece CreateKing(Color color)
        {
            var king = new Piece(PieceType.King, color);
            foreach (var dir in Directions.All)
            {
                king.m_moveRules[dir] = StraightMovementRule.OneSpace;
            }
            //TODO: Castling

            return king;
        }

        public static Piece CreateQueen(Color color)
        {
            var queen = new Piece(PieceType.Queen, color);
            foreach (var dir in Directions.All)
            {
                queen.m_moveRules[dir] = StraightMovementRule.Infinite;
            }

            return queen;
        }

        public static Piece CreateBishop(Color color)
        {
            var bishop = new Piece(PieceType.Bishop, color);
            foreach (var dir in Directions.All)
            {
                bishop.m_moveRules[dir] = dir.IsDiagonal()
                    ? StraightMovementRule.Infinite
                    : StraightMovementRule.None;
            }

            return bishop;
        }

        public static Piece CreateRook(Color color)
        {
            var rook = new Piece(PieceType.Rook, color);
            foreach (var dir in Directions.All)
            {
                rook.m_moveRules[dir] = dir.IsCardinal()
                    ? StraightMovementRule.Infinite
                    : StraightMovementRule.None;
            }

            return rook;
        }

        public static Piece CreateKnight(Color color)
        {
            var knight = new Piece(PieceType.Knight, color);
            foreach (var dir in Directions.All)
            {
                knight.m_moveRules[dir] = KnightMovementRule.Instance;
            }

            return knight;
        }

        /// <summary>
        /// Create a pawn with its rules
        /// </summary>
        /// <param name="color">The color of the piece</param>
        /// <param name="dir">The direction the piece faces</param>
        /// <returns></returns>
        public static Piece CreatePawn(Color color, Direction dir)
        {
            var pawn = new Piece(PieceType.Pawn, color)
            {
                AttackRules = new Dictionary<Direction, IMovementRule>(),
                m_firstMoveRules = new Dictionary<Direction, IMovementRule>()
            };

            var attacks = new []
            {
                dir.RotateClockwise(),
                dir.RotateCounterClockwise()
            };

            foreach (var d in Directions.All)
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
            var blank = new Piece(PieceType.Empty, Color.None);
            foreach (var d in Directions.All)
            {
                blank.m_moveRules[d] = StraightMovementRule.None;
            }

            return blank;
        }
    }
}
