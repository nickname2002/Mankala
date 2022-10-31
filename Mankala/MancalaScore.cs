using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class MancalaScore : IScore
    {
        public MancalaScore()
        {

        }

        public bool GameOver(Board board)
        {
            throw new NotImplementedException();
        }

        public Player GetWinner(Board board)
        {
            throw new NotImplementedException();
        }

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit lastPit)
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
    }
}
