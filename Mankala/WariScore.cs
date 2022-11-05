using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class WariScore : IScore
    {
        public bool GameOver(Board board)
        {
            // Draw
            if (IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                return true;
            }

            // Left player won
            if (IsOnlyWinner(board, board.HomePitLeft))
            {
                return true;
            }

            // Right player won
            if (IsOnlyWinner(board, board.HomePitRight))
            {
                return true;
            }

            return false;
        }

        public Player? GetWinner(Board board)
        {
            if (IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                return null;
            }

            if (IsOnlyWinner(board, board.HomePitLeft))
            {
                return board.HomePitLeft.Owner;
            }

            return board.HomePitRight.Owner;
        }

        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight)
        {
            if (homePitLeft.StonesAmount == homePitRight.StonesAmount
                   && homePitRight.StonesAmount == board.TotalStonesAmount / 2)
            {
                return true;
            }

            return false;
        }

        public bool IsOnlyWinner(Board board, Pit homePit)
        {
            // TODO: Needs additional rules

            if (homePit.StonesAmount > (board.TotalStonesAmount / 2))
            {
                return true;
            }

            return false;
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit)
        {
            if (lastPit.IndexInList == startPit.IndexInList)
            {
                return cPlayer;
            }

            if (cPlayer == p1)
            {
                return p2;
            }

            return p1;
        }
    }
}
