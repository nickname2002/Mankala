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
        public Player activePlayer;

        // Board
        public Board board;

        // Mancala game strategies
        IScore scoreStrategy;
        ITurn turnStrategy; 

        public MancalaGame()
        {
            // NOTE: Placeholder values
            this.board = new Board(6, 4);
            this.mancalaFactory = new MancalaFactory();
            this.scoreStrategy = mancalaFactory.CreateScore();
            this.turnStrategy = mancalaFactory.CreateTurn();

            this.p1 = new Player("P1", board.HomePitRight, board.HomePitLeft);
            this.p2 = new Player("P2", board.HomePitLeft, board.HomePitRight);
            this.activePlayer = p1;
        }

        public void DrawScore(Graphics gr)
        {
            gr.DrawString($"Active player: {this.activePlayer}", new Font("Arial", 16), Brushes.Black, new Point(100, 50));
        }

        public void PerformTurn(Point mouseLoc)
        {
            Pit? clickedPit = board.ClickPit(mouseLoc);

            if (clickedPit == null)
            {
                return;
            }

            Pit lastPit = turnStrategy.PerformTurn(board, activePlayer, clickedPit);
            this.activePlayer = scoreStrategy.SwitchPlayer(activePlayer, p1, p2, lastPit);
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
