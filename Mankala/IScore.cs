using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Mancala
{
    public interface IScore
    {
        public bool GameOver(ITurn turnStrategy, Board board);
        public Player? GetWinner(Board board);
        public bool IsDraw(Board board, Pit homePitLeft, Pit homePitRight);
        public bool IsOnlyWinner(Player cPlayer);
        public Player SwitchPlayer(Player cPlayer, Player p1, Player p2, Pit startPit, Pit lastPit);
        public void CheckForEmptyRow(Board board, Player cPlayer);
        public bool WinningStonesAmountReached(Board board);

    }
}
