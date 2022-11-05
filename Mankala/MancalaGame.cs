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

        public MancalaGame(IMancalaFactory game)
        {
            this.mancalaFactory = game;

            // Initialize board with factory
            this.board = mancalaFactory.CreateBoard();

            // Initialize strategies with factory
            this.scoreStrategy = mancalaFactory.CreateScore();
            this.turnStrategy = mancalaFactory.CreateTurn();

            // Initialize players
            this.p1 = new Player("P1", board.HomePitRight, board.HomePitLeft);
            this.p2 = new Player("P2", board.HomePitLeft, board.HomePitRight);
            this.activePlayer = p1;

            // Assign the owners to the home pits
            this.board.HomePitLeft.Owner = this.p1;
            this.board.HomePitRight.Owner = this.p2;
        }

        public void DrawScore(Graphics gr)
        {
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gr.DrawString($"Active player: {this.activePlayer}", new Font("Trebuchet MS", 16), Brushes.Black, new Point(20, 20));

            if (GameOver())
            {
                DrawGameOver(gr);
            }
        }

        public void DrawGameOver(Graphics gr)
        {
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (scoreStrategy.IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                gr.DrawString("Draw!", new Font("Trebuchet MS", 50), Brushes.Purple, new Point(200, 500));
                return;
            }

            gr.DrawString($"{scoreStrategy.GetWinner(board)} has won!", new Font("Trebuchet MS", 50), Brushes.Purple, new Point(200, 500));
        }

        public void PerformTurn(Point mouseLoc)
        {
            Pit? clickedPit = board.ClickPit(mouseLoc);

            if (clickedPit == null)
            {
                return;
            }

            if (clickedPit.IsEmpty())
            {
                return;
            }

            if (!activePlayer.IsOwnedPit(clickedPit))
            {
                return;
            }

            Pit lastPit = turnStrategy.PerformTurn(board, activePlayer, clickedPit);

            this.activePlayer = scoreStrategy.SwitchPlayer(activePlayer, p1, p2, lastPit);
        }

        public bool GameOver()
        {
            if (scoreStrategy.GameOver(board))
            {
                return true;
            }

            return false;
        }

        public bool ResetGame()
        {
            throw new NotImplementedException();
        }
    }
}
