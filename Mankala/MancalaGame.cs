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
            this.board.HomePitLeft.Owner = this.p2;
            this.board.HomePitRight.Owner = this.p1;
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
            Font bodoniSmall = new Font("Bodoni MT", 25);
            Font bodoniLarge = new Font("Bodoni MT", 30);

            // Draw message box
            gr.FillRectangle(Brushes.Chocolate, 800 / 2 - 210, 600 / 2 - 160, 420, 320);
            gr.FillRectangle(Brushes.Sienna, 800 / 2 - 200, 600 / 2 - 150, 400, 300);

            // Display game state update and method to go to leave the game
            gr.DrawString("~   GAME OVER   ~", bodoniLarge, Brushes.Gold, new Point(210, 170));
            gr.DrawString("Press 'c' to continue.", bodoniSmall, Brushes.Gold, new Point(250, 395));

            // Display message when the game resulted in a draw
            if (scoreStrategy.IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                gr.DrawString("The game resulted\n        in a draw!", bodoniLarge, Brushes.Gold, new Point(230, 260));
                return;
            }

            // Display message to show which player has won
            gr.DrawString($"{scoreStrategy.GetWinner(board)} has won!", bodoniLarge, Brushes.Gold, new Point(290, 260));
        }

        public void PerformTurn(Point mouseLoc)
        {
            if (GameOver())
            {
                return;
            }

            Pit? clickedPit = board.ClickPit(mouseLoc);

            if (clickedPit == null 
                || clickedPit.IsEmpty() 
                || !activePlayer.IsOwnedPit(clickedPit))
            {
                return;
            }

            Pit lastPit = turnStrategy.PerformTurn(board, activePlayer, clickedPit);
            this.activePlayer = scoreStrategy.SwitchPlayer(activePlayer, p1, p2, clickedPit, lastPit);
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
