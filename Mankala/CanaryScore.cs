using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class CanaryScore : IScore
    {
        public CanaryScore()
        {

        }

        public void CheckForEmptyRow(Board board, Player cPlayer)
        {
            throw new NotImplementedException();
        }

        public bool GameOver(ITurn turnStrategy, Board board)
        {
            throw new NotImplementedException();
        }

        public Player? GetWinner(Board board)
        {
            throw new NotImplementedException();
        }

        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight)
        {
            throw new NotImplementedException();
        }

        public bool IsOnlyWinner(Player cPlayer)
        {
            throw new NotImplementedException();
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit)
        {
            throw new NotImplementedException();
        }

        public bool WinningStonesAmountReached(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
