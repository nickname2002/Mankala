using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class MancalaTurn : ITurn
    {
        public void PerformTurn(Board board, Player cPlayer, Pit startingPit)
        {
            Pit cPit = startingPit;

            // Get stones from a pit
            int stonesAmount = startingPit.GetStone(); 

            // Move to pits in counterclockwise direction, add one stone
            while (stonesAmount != 0)
            {
                cPit = board.NextPit(cPit);

                // When hovering over an opposing homepit, don't add a stone
                if (cPit.IndexInList == cPlayer.OpposingHomePit.IndexInList)
                {
                    continue;
                }

                cPit.AddStone();
                stonesAmount--;
            }
        }
    }
}
