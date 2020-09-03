using System.Drawing;

namespace Chess.Server.Model
{
    public struct Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        public static implicit operator Point(Location l) => new Point(l.X, l.Y);
        public static implicit operator Location(Point p) => new Location(p.X, p.Y);

        public override string ToString()=> $"({X},{Y})";
    }
}