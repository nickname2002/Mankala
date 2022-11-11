using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Mancala
{
    public class Player
    {
        private readonly string strRep;
        private readonly Pit opposingHomePit;

        // Player properties
        public Pit HomePit { get; }
        public Pit OpposingHomePit => opposingHomePit;
        public Player Opponent => ((HomePit)opposingHomePit).Owner;

        public Player(string name, Pit homePit, Pit opposingHomePit)
        {
            this.strRep = name;
            this.HomePit = homePit;
            this.opposingHomePit = opposingHomePit;
        }

        public override string ToString()
        {
            return this.strRep;
        }
    }
}
