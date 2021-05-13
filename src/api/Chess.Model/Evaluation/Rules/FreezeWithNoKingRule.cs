using Chess.Model.Evaluation.Models;

namespace Chess.Model.Evaluation.Rules
{
    public class FreezeWithNoKingRule : AbstractPathRule
    {
        public FreezeWithNoKingRule(IPathRule chain) : base(chain)
        {
        }

        public bool Enabled { get; set; }

        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (Enabled && !markings.KingLocations.ContainsKey(path.Piece.Color))
            {
                System.Console.WriteLine($"{Enabled}::{path.Piece}");
                return;
            }
            base.Apply(markings, path);
        }
    }
}