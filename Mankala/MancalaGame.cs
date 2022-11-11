using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mankala
{
    public enum GameType { Mancala, Wari }
    public enum PlayerID { P1, P2 }

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
            this.p1 = mancalaFactory.CreatePlayer(board, PlayerID.P1);
            this.p2 = mancalaFactory.CreatePlayer(board, PlayerID.P2);
            this.activePlayer = p1;
        }

        public void DrawScore(Graphics gr)
        {
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gr.DrawString($"Active player:\n{this.activePlayer}", new Font("Trebuchet MS", 16), Brushes.Black, new Point(20, 20));

            if (GameOver())
            {
                DrawGameOver(gr);
            }
        }

        public void DrawGameOver(Graphics gr)
        {
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font bodoniSmall = new Font("Bodoni MT", 20);
            Font bodoniLarge = new Font("Bodoni MT", 25);

            // Draw message box
            gr.FillRectangle(Brushes.Chocolate, 800 / 2 - 205, 5, 410, 150);
            gr.FillRectangle(Brushes.Sienna, 800 / 2 - 200, 10, 400, 140);

            // Display method to leave the game
            gr.DrawString("Press 'c' to continue.", bodoniSmall, Brushes.Gold, new Point(280, 90));

            // Display message when the game resulted in a draw
            if (scoreStrategy.IsDraw(board, board.HomePitLeft, board.HomePitRight))
            {
                gr.DrawString("It's a Draw!", bodoniLarge, Brushes.Gold, new Point(275, 30));
                return;
            }

            // Display message to show which player has won
            gr.DrawString($"{scoreStrategy.GetWinner(board)} has won!", bodoniLarge, Brushes.Gold, new Point(310, 30));
        }

        public void PerformTurn(Point mouseLoc)
        {
            if (GameOver())
            {
                return;
            }

            Pit? clickedPit = board.ClickPit(mouseLoc);

            // Checks whether a valid pit is clicked
            if (clickedPit == null
                || clickedPit.IsEmpty() 
                || !turnStrategy.PitOwnedByPlayer(activePlayer, clickedPit))
            {
                return;
            }

            SwitchPlayer(clickedPit);
        }

        /* Switch player turns */
        private void SwitchPlayer(Pit clickedPit)
        {
            Pit lastPit = turnStrategy.PerformTurn(board, activePlayer, clickedPit);
            this.activePlayer = scoreStrategy.SwitchPlayer(activePlayer, p1, p2, clickedPit, lastPit);
        }

        public bool GameOver()
        {
            if (scoreStrategy.GameOver(turnStrategy, board))
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
