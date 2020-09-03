namespace Chess.Server.Model
{
    public class Move
    {
        public Location From { get; set; }
        public Location To { get; set; }

        public override string ToString() => $"{From}=>{To}";
    }
}