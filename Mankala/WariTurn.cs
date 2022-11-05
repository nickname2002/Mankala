using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class WariTurn : ITurn
    {
        public Pit NextPit(Board board, Pit cPit)
        {
            // When on edge of the board, move to opposing row
            if (cPit.IndexInList == 1 || cPit.IndexInList == board.PlayPitsTotal)
            {
                return board.OpposingPit(cPit);
            }

            // Top play pits
            if (cPit.IndexInList <= board.PlaysPitPerRow)
            {
                return board.GetPit(cPit.IndexInList - 1);
            }

            // Bottom play pits
            return board.GetPit(cPit.IndexInList + 1);
        }

        public Pit PreviousPit(Board board, Pit cPit)
        {
            // When on edge of the board, move to opposing row
            if (cPit.IndexInList == board.PlaysPitPerRow + 1 || cPit.IndexInList == board.PlaysPitPerRow)
            {
                return board.OpposingPit(cPit);
            }

            // Top play pits
            if (cPit.IndexInList <= board.PlaysPitPerRow)
            {
                return board.GetPit(cPit.IndexInList + 1);
            }

            // Bottom play pits
            return board.GetPit(cPit.IndexInList - 1);
        }

        public void CaptureSeeds(Board board, Player cPlayer, Pit cPit)
        {            
            if ((cPit.StonesAmount == 2 || cPit.StonesAmount == 3) && !cPlayer.IsOwnedPit(cPit))
            {
                int stonesToGain = cPit.GetStone();
                Pit prevPit = PreviousPit(board, cPit);

                // Move stones to homepit player 
                cPit.RemoveStone();
                cPlayer.HomePit.Fill(stonesToGain);

                CaptureSeeds(board, cPlayer, prevPit);
            }
        }

        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit)
        {
            // Play cup more than 11 stones -> pass when this cup occurs
            // Capturing pits when opponent pit and contains 2 or 3 stones
            Pit cPit = startingPit;

            // Get stones from a pit
            int stonesAmount = startingPit.GetStone();
            startingPit.RemoveStone();

            // Move to pits in counterclockwise direction, add one stone
            while (stonesAmount != 0)
            {
                cPit = NextPit(board, cPit);
                cPit.AddStone();
                stonesAmount--;
            }

            // Action performed when on last pit of move
            CaptureSeeds(board, cPlayer, cPit);

            return cPit;
        }
    }
}
