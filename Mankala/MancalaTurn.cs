using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class MancalaTurn : ITurn
    {
        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit)
        {
            Pit cPit = startingPit;

            // Get stones from a pit
            int stonesAmount = startingPit.GetStone();
            startingPit.RemoveStone();

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

            return cPit;
        }

        public void OnEmptyFriendlyAction(Board board, Player cPlayer, Pit cPit)
        {
            if (cPit.ToString() == "HomePit")
            {
                return;
            }

            if (cPit.StonesAmount == 1 && cPlayer.IsOwnedPit(cPit))
            {
                // Get all stones from opposing pit
                Pit opposingPit = board.OpposingPit(cPit);
                int stonesToGain = opposingPit.StonesAmount;

                // Move all stones from opposing pit to homepit
                opposingPit.RemoveStone();
                cPlayer.HomePit.Fill(stonesToGain);                
            }
        }
    }
}
