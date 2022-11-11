using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class CanaryScore : IScore
    {
        public bool GameOver(Board board)
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

        public bool IsOnlyWinner(Board board, Pit homepit)
        {
            throw new NotImplementedException();
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit)
        {
            throw new NotImplementedException();
        }
    }
}
