using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Mankala
{
    public class Player
    {
        private string strRep;
        private Pit homePit;
        private Pit opposingHomePit;

        public Pit HomePit
        {
            get 
            { 
                return homePit; 
            }
        }

        public Pit OpposingHomePit
        {
            get
            {
                return opposingHomePit;
            }
        }

        public Player(string name, Pit homePit, Pit opposingHomePit)
        {
            this.strRep = name;
            this.homePit = homePit;
            this.opposingHomePit = opposingHomePit;
        }

        public override string ToString()
        {
            return this.strRep;
        }
    }
}
