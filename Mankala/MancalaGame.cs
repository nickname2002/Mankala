using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mankala
{
    enum GameType { Mancala, Wari }

    public class MancalaGame
    {
        // Mancala factory
        IMancalaFactory mancalaFactory;

        // Player data 
        Player p1;
        Player p2;
        Player activePlayer;

        // Board
        public Board board;

        // Mancala game strategies
        IScore scoreStrategy;
        ITurn turnStrategy; 

        public MancalaGame()
        {
            this.p1 = new Player();
            this.p2 = new Player();

            // NOTE: placeholder board for testing purposes
            this.board = new Board(6);
        }

        public Board PerformTurn(Player p, Board b)
        {
            throw new NotImplementedException();
        }

        public bool GameOver()
        {
            throw new NotImplementedException();
        }

        public bool ResetGame()
        {
            throw new NotImplementedException();
        }
    }
}
