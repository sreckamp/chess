using System.Collections.Generic;
using Chess.Model.Models;

namespace Chess.Server.Model
{
    public class Metadata
    {
        public IEnumerable<Marker> Markers { get; set; }
    }
}