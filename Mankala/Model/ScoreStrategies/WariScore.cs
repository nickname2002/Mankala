using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class WariScore : IScore
    {
        public void CheckForEmptyRow(ITurn turnStrategy, Board board, Player cPlayer)
        {
            if (board.IsEmptyRow(cPlayer))
            {
                board.TransferToHomePit(turnStrategy, cPlayer.Opponent);
            }
        }

        public bool GameOver(ITurn turnStrategy, Board board)
        {
            Player p1 = board.HomePitRight.Owner;
            Player p2 = board.HomePitLeft.Owner;

            // Check for empty rows for each player
            CheckForEmptyRow(turnStrategy, board, p1);
            CheckForEmptyRow(turnStrategy, board, p2);

            if (!WinningStonesAmountReached(board))
            {
                return false;
            }

            return IsDraw(board, p2.HomePit, p1.HomePit) || IsOnlyWinner(p1) || IsOnlyWinner(p2);
        }

        public Player? GetWinner(Board board)
        {
            if (IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                return null;
            }

            return IsOnlyWinner(board.HomePitLeft.Owner) ? board.HomePitLeft.Owner : board.HomePitRight.Owner;
        }

        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight)
        {
            return homePitLeft.StonesAmount == homePitRight.StonesAmount;
        }

        public bool IsOnlyWinner(Player cPlayer)
        {
            return cPlayer.HomePit.StonesAmount > cPlayer.OpposingHomePit.StonesAmount;
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit)
        {
            if (lastPit.IndexInList == startPit.IndexInList)
            {
                return cPlayer;
            }

            return cPlayer == p1 ? p2 : p1;
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
