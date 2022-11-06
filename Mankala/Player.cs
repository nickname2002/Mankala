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

        public Player Opponent
        {
            get
            {
                return ((HomePit)opposingHomePit).Owner;
            }
        }

        public Player(string name, Pit homePit, Pit opposingHomePit)
        {
            this.strRep = name;
            this.homePit = homePit;
            this.opposingHomePit = opposingHomePit;
        }

        /* Checks if a certain pit is owned by the player */
        public bool IsOwnedPit(Pit sPit)
        {
            int indexSelectedPit = sPit.IndexInList;

            if (this.homePit.IndexInList == 0)
            {
                if (indexSelectedPit <= this.OpposingHomePit.IndexInList / 2)
                {
                    return true;
                }
            }
            else
            {
                if (indexSelectedPit > this.homePit.IndexInList / 2)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return this.strRep;
        }
    }
}
