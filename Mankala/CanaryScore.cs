using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class CanaryScore : IScore
    {
        public void CheckForEmptyRow(Board board, Player cPlayer)
        {
            if (board.IsEmptyRow(cPlayer))
            {
                board.TransferToHomePit(cPlayer.Opponent);
            }
        }

        public bool GameOver(ITurn turnStrategy, Board board)
        {
            Player p1 = board.HomePitLeft.Owner;
            Player p2 = board.HomePitRight.Owner;

            // Check empty rows for each player
            CheckForEmptyRow(board, p1);
            CheckForEmptyRow(board, p2);

            if (WinningStonesAmountReached(board))
            {
                if (IsDraw(board, p2.HomePit, p1.HomePit) || IsOnlyWinner(p1) || IsOnlyWinner(p2))
                {
                    return true;
                }
            }

            return false;
        }

        public Player? GetWinner(Board board)
        {
            if (IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                return null;
            }

            if (IsOnlyWinner(board.HomePitLeft.Owner))
            {
                return board.HomePitLeft.Owner;
            }

            return board.HomePitRight.Owner;
        }

        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight)
        {
            if (homePitLeft.StonesAmount == homePitRight.StonesAmount)
            {
                return true;
            }

            return false;
        }

        public bool IsOnlyWinner(Player cPlayer)
        {
            if (cPlayer.HomePit.StonesAmount > cPlayer.OpposingHomePit.StonesAmount)
            {
                return true;
            }

            return false;
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit)
        {
            if (lastPit == cPlayer.HomePit)
            {
                return cPlayer;
            }

            if (cPlayer == p1)
            {
                return p2;
            }

            return p1;
        }

        public bool WinningStonesAmountReached(Board board)
        {
            int winningStonesAmount = board.TotalStonesAmount / 2;
            int p1StonesAmount = board.HomePitRight.StonesAmount;
            int p2StonesAmount = board.HomePitLeft.StonesAmount;

            return p1StonesAmount > winningStonesAmount || p2StonesAmount > winningStonesAmount;
        }
    }
}
