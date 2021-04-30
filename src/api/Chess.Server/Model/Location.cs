using System.Drawing;

namespace Chess.Server.Model
{
    public class Location
    {
        public Location()
        {
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        public Metadata Metadata { get; set; }
        
        public static implicit operator Point(Location l) => new Point(l.X, l.Y);
        public static implicit operator Location(Point p) => new Location {X = p.X, Y = p.Y};

        public override string ToString()=> $"({X},{Y})";
    }
}