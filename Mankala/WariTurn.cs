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

        public bool MovePossible(Board board, Player cPlayer)
        {
            if (board.IsEmptyRow(cPlayer))
            {
                return false;
            }

            return true;
        }

        public bool IsValidTurn(Board board, Player cPlayer)
        {
            if (board.IsEmptyRow(cPlayer.Opponent))
            {
                return false;
            }

            return true;
        }

        public void CaptureSeeds(Board board, Player cPlayer, Pit cPit)
        {            
            if ((cPit.StonesAmount == 2 || cPit.StonesAmount == 3) && !PitOwnedByPlayer(board, cPlayer, cPit))
            {
                int stonesToGain = cPit.GetStones();
                Pit prevPit = PreviousPit(board, cPit);

                // Move stones to homepit player 
                cPit.RemoveStones();
                cPlayer.HomePit.Fill(stonesToGain);

                CaptureSeeds(board, cPlayer, prevPit);
            }
        }

        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit)
        {
            (Board, Pit) performedMove = PerformDummyTurn(board, cPlayer, startingPit);

            if (IsValidTurn(performedMove.Item1, cPlayer))
            {
                Pit cPit = startingPit;

                // Get stones from a pit
                int stonesAmount = startingPit.GetStones();
                startingPit.RemoveStones();

                // Move to pits in counterclockwise direction, add one stone
                while (stonesAmount != 0)
                {
                    cPit = NextPit(board, cPit);

                    // If the current pit is equal to the starting pit of the move, continue
                    if (cPit == startingPit)
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

            return startingPit;
        }

        public (Board, Pit) PerformDummyTurn(Board board, Player cPlayer, Pit startingPit)
        {
            Pit copyStartingPit = startingPit.Clone();
            Board dummyBoard = board.Clone();

            // Get stones from a pit
            int stonesAmount = copyStartingPit.GetStones();
            copyStartingPit.RemoveStones();

            Pit cPit = copyStartingPit;

            // Move to pits in counterclockwise direction, add one stone
            while (stonesAmount != 0)
            {
                cPit = NextPit(dummyBoard, cPit);

                // If the current pit is equal to the starting pit of the move, continue
                if (cPit == copyStartingPit)
                {
                    continue;
                }

                cPit.AddStone();
                stonesAmount--;
            }

            // Action performed when on last pit of move
            CaptureSeeds(dummyBoard, cPlayer, cPit);

            return (dummyBoard, cPit);
        }
    }
}
