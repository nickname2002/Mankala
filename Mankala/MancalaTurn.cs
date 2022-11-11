using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class MancalaTurn : ITurn
    {
        public bool MovePossible(Board board, Player cPlayer)
        {
            return !board.IsEmptyRow(cPlayer);
        }

        public Pit NextPit(Board board, Pit cPit)
        {
            // Left home pit
            if (cPit.IndexInList == board.HomePitLeft.IndexInList)
            {
                return board.GetPit(board.PlaysPitPerRow + 1);
            }

            // Right home pit
            if (cPit.IndexInList == board.HomePitRight.IndexInList)
            {
                return board.GetPit(board.PlaysPitPerRow);
            }

            // Top play pits
            if (cPit.IndexInList <= board.PlaysPitPerRow)
            {
                return board.GetPit(cPit.IndexInList - 1);
            }

            // Bottom play pits
            return board.GetPit(cPit.IndexInList + 1);
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

            // Move to pits in counterclockwise direction, add one stone
            while (stonesAmount != 0)
            {
                cPit = NextPit(board, cPit);

                // When hovering over an opposing home pit, don't add a stone
                if (cPit.IndexInList == cPlayer.OpposingHomePit.IndexInList)
                {
                    continue;
                }

                cPit.AddStone();
                stonesAmount--;
            }

            // Action performed when on last pit of move
            CaptureSeeds(board, cPlayer, cPit);

            return cPit;
        }

        public void CaptureSeeds(Board board, Player cPlayer, Pit cPit)
        {
            if (cPit.ToString() == "HomePit")
            {
                return;
            }

            if (cPit.StonesAmount != 1 || !PitOwnedByPlayer(board, cPlayer, cPit))
            {
                return;
            }

            // Get all stones from opposing pit
            Pit opposingPit = board.OpposingPit(cPit);
            int stonesToGain = opposingPit.GetStones();

            // Move all stones from opposing pit to home pit
            opposingPit.RemoveStones();
            cPlayer.HomePit.Fill(stonesToGain);
        }

        public bool PitOwnedByPlayer(Board board, Player cPlayer, Pit cPit)
        {
            int indexSelectedPit = cPit.IndexInList;

            if (cPlayer.HomePit.IndexInList == 0)
            {
                if (indexSelectedPit <= cPlayer.OpposingHomePit.IndexInList / 2)
                {
                    return true;
                }
            }
            else
            {
                if (indexSelectedPit > cPlayer.HomePit.IndexInList / 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
