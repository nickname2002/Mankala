using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class CanaryTurn : ITurn
    {
        public CanaryTurn()
        {

        }

        public void CaptureSeeds(Board board, Player cPlayer, Pit cPit)
        {
            if (cPit.ToString() == "HomePit")
            {
                return;
            }

            // Fetch all stones from current and opposing play pit when their stones amount are equal
            if (cPit.StonesAmount == board.OpposingPit(cPit).StonesAmount)
            {
                // Get all stones from opposing and current pit
                Pit opposingPit = board.OpposingPit(cPit);
                int stonesToGain = opposingPit.GetStones() + cPit.GetStones();

                // Move all stones from play pits to homepit
                opposingPit.RemoveStones();
                cPit.RemoveStones();
                cPlayer.HomePit.Fill(stonesToGain);
            }
        }

        public bool MovePossible(Board board, Player cPlayer)
        {
            if (board.IsEmptyRow(cPlayer))
            {
                return false;
            }

            return true;
        }

        public Pit NextPit(Board board, Pit cPit)
        {
            // Left homepit
            if (cPit.IndexInList == board.PlaysPitPerRow + 1)
            {
                return board.HomePitLeft;
            }

            // Right homepit
            if (cPit.IndexInList == board.PlaysPitPerRow)
            {
                return board.HomePitRight;
            }

            // Top play pits
            if (cPit.IndexInList < board.PlaysPitPerRow)
            {
                return board.GetPit(cPit.IndexInList + 1);
            }

            // Bottom play pits
            return board.GetPit(cPit.IndexInList - 1);
        }

        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit)
        {
            if (startingPit.ToString() == "HomePit")
            {
                return startingPit;
            }

            Pit cPit = startingPit;

            // Get stones from a pit
            int stonesAmount = startingPit.GetStones();
            startingPit.RemoveStones();

            // Move to pits in clockwise direction, adding one stone
            while (stonesAmount != 0)
            {
                cPit = NextPit(board, cPit);

                // When hovering over an opposing homepit, don't add a stone
                if (cPit.IndexInList == cPlayer.OpposingHomePit.IndexInList)
                {
                    continue;
                }

                cPit.AddStone();
                stonesAmount--;
            }

            // Action performed whne on last pit of move
            CaptureSeeds(board, cPlayer, cPit);

            return cPit;
        }

        public bool PitOwnedByPlayer(Board board, Player cPlayer, Pit cPit)
        {
            int indexSelectedPit = cPit.IndexInList;

            // Check for left home pit owner
            if (cPlayer.HomePit.IndexInList == 0)
            {
                if (indexSelectedPit >= board.PlaysPitPerRow + 1 
                    && indexSelectedPit != board.HomePitRight.IndexInList)
                {
                    return true;
                }
            }
            // Check for right homepit owner
            else
            {
                if (indexSelectedPit <= board.PlaysPitPerRow && indexSelectedPit != board.HomePitLeft.IndexInList)
                {
                    return true;
                }
            }

            return false;
        }
    }
}