using System.Collections.Generic;
using Chess.Model.Models;

namespace Chess.Server.Model
{
    public class Metadata
    {
        public IEnumerable<AttackData> AttackedBy { get; set; }
        public string PinDir { get; set; }
    }
}