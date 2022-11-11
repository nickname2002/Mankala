using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class MancalaScore : IScore
    {
        public bool GameOver(ITurn turnStrategy, Board board)
        {
            Player p1 = board.HomePitLeft.Owner;
            Player p2 = board.HomePitRight.Owner;

            // Check empty rows for each player
            CheckForEmptyRow(board, p1);
            CheckForEmptyRow(board, p2);

            if (!WinningStonesAmountReached(board))
            {
                return false;
            }

            return IsDraw(board, p2.HomePit, p1.HomePit) || IsOnlyWinner(p1) || IsOnlyWinner(p2);
        }

        public void CheckForEmptyRow(Board board, Player cPlayer)
        {
            if (board.IsEmptyRow(cPlayer))
            {
                board.TransferToHomePit(cPlayer.Opponent);
            }
        }

        public bool WinningStonesAmountReached(Board board)
        {
            int winningStonesAmount = board.TotalStonesAmount / 2;
            int p1StonesAmount = board.HomePitRight.StonesAmount;
            int p2StonesAmount = board.HomePitLeft.StonesAmount;

            return p1StonesAmount > winningStonesAmount || p2StonesAmount > winningStonesAmount;
        }

        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight)
        {
            return homePitLeft.StonesAmount == homePitRight.StonesAmount;
        }

        public bool IsOnlyWinner(Player cPlayer)
        {
            return cPlayer.HomePit.StonesAmount > cPlayer.OpposingHomePit.StonesAmount;
        }

        public Player? GetWinner(Board board)
        {
            if (IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                return null;
            }

            return IsOnlyWinner(board.HomePitLeft.Owner) ? board.HomePitLeft.Owner : board.HomePitRight.Owner;
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit)
        {
            if (lastPit == cPlayer.HomePit)
            {
                return cPlayer;
            }

            return cPlayer == p1 ? p2 : p1;
        }
    }
}
