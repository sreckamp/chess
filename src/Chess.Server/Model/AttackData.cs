using System.Collections.Generic;

namespace Chess.Server.Model
{
    public class AttackData
    {
        public string Direction { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}