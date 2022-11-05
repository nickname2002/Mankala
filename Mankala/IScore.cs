using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Mankala
{
    public interface IScore
    {
        public bool GameOver(Board board);
        public Player? GetWinner(Board board);
        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight);
        public bool IsOnlyWinner(Board board, Pit homepit);

        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit);
    }
}
