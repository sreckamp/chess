namespace Chess.Server.Model
{
    public class Piece
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public Location Location { get; set; }
        public override string ToString() => $"{Color} {Type}@{Location})";
    }
}