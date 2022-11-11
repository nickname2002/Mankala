using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mancala
{
    public class Board
    {
        // Constant values
        private const int OFFSET_TO_BORDER = 20;
        private const int PIT_OFFSET = 10;

        // Array containing pits in board
        private Pit[] pits;

        // Pit dimensions
        private int homePitWidth;
        private int homePitHeight;
        private int playPitWidth;
        private int playPitHeight;

        // Starting number of stones per pit
        private int startingStonesAmount;
        private int playPitsPerRow;

        // Board graphical properties
        private Point coords;
        private int width;
        private int height;

        // Board properties
        public HomePit HomePitLeft => (HomePit)pits[0];
        public HomePit HomePitRight => (HomePit)pits[this.PitsTotal - 1];
        public int PlaysPitPerRow => playPitsPerRow;
        public int PlayPitsTotal => PlaysPitPerRow * 2;
        public int PitsTotal => PlaysPitPerRow * 2 + 2;
        public int TotalStonesAmount => startingStonesAmount * this.PlayPitsTotal;

        public Board(int size, int startingStonesAmount)
        {
            // Init board size
            this.playPitsPerRow = size;

            // Arrange array of pits
            this.startingStonesAmount = startingStonesAmount;
            this.pits = new Pit[(PlaysPitPerRow * 2) + 2];
            this.FillBoard();

            // Init pit sizes
            this.homePitWidth = pits[0].Width;
            this.homePitHeight = pits[0].Height;
            this.playPitWidth = pits[1].Width;
            this.playPitHeight = pits[1].Height;

            // Set dimensions of board 
            this.width = (pits[0].Width * 2) + (pits[0].Width * this.PlaysPitPerRow) + (PIT_OFFSET * (this.PlaysPitPerRow + 1)) + (2 * OFFSET_TO_BORDER);
            this.height = (pits[0].Height) + this.PlaysPitPerRow + OFFSET_TO_BORDER * 2;

            // Set coordinates of board
            this.coords.X = (800 - this.width) / 2;
            this.coords.Y = (600 - this.height) / 2;
        }

        /* Fill board with pits */
        private void FillBoard()
        {
            // Initialize play pits
            for (int i = 0; i < this.PitsTotal; i++)
            {
                if (i == 0 || i == this.PitsTotal - 1)
                {
                    this.pits[i] = new HomePit(i);
                    continue;
                }

                this.pits[i] = new PlayPit(i);
            }

            // Add stones to the pits
            ResetBoard();
        }

        /* Checks whether a pit is clicked */
        public Pit? ClickPit(Point mouseLoc)
        {
            return this.pits.FirstOrDefault(pit => pit.Clicked(mouseLoc));
        }

        /* Draw the full board */
        public void Draw(Graphics gr)
        {
            DrawBoardBackdrop(gr);
            DrawHomePits(gr);
            DrawPlayPits(gr);
        }

        /* Draw backdrop of the board */
        public void DrawBoardBackdrop(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.FillRectangle(Brushes.Peru, coords.X, coords.Y - OFFSET_TO_BORDER, this.width, this.height);
        }

        /* Drawing home pits */
        private void DrawHomePits(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Determine coordinates of each pit
            HomePitLeft.ScreenLoc = new Point(OFFSET_TO_BORDER + coords.X, coords.Y);            
            HomePitRight.ScreenLoc = new Point(OFFSET_TO_BORDER + coords.X + PIT_OFFSET + homePitWidth + homePitWidth * PlaysPitPerRow + PIT_OFFSET * PlaysPitPerRow, coords.Y);

            HomePitLeft.Draw(gr);
            HomePitRight.Draw(gr);
        }

        /* Get play pits of a specific player */
        public List<Pit> GetPlayPits(Player owner)
        {
            List<Pit> playPits = new List<Pit>();

            switch (owner.ToString())
            {
                // Check for P1
                case "P1":
                {
                    for (int i = this.playPitsPerRow + 1; i <= this.HomePitRight.IndexInList - 1; i++)
                    {
                        playPits.Add(pits[i]);
                    }

                    break;
                }
                // Check for P2
                case "P2":
                {
                    for (int i = 1; i <= this.playPitsPerRow; i++)
                    {
                        if (this.pits[i].GetStones() != 0)
                        {
                            playPits.Add(pits[i]);
                        }
                    }

                    break;
                }
            }

            return playPits;
        }

        /* Drawing play pits */
        private void DrawPlayPits(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw play pits
            for (int i = 0; i < PlaysPitPerRow * 2; i++)
            {
                Pit cPit = this.pits[i + 1];

                // Position of pit
                int posX;
                int posY;
                int offsetToLeft;

                // Determine coordinates of all play pits

                // Top play pits
                if (i < this.playPitsPerRow)
                {
                    offsetToLeft = playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i;
                    posX = OFFSET_TO_BORDER + coords.X + offsetToLeft;
                    posY = coords.Y;
                }
                // Bottom play pits
                else
                {
                    offsetToLeft = playPitWidth + playPitWidth * (i - playPitsPerRow - 1) + PIT_OFFSET * (i - playPitsPerRow - 1);
                    posX = homePitWidth + OFFSET_TO_BORDER + OFFSET_TO_BORDER + coords.X + offsetToLeft;
                    posY = coords.Y + playPitWidth + PIT_OFFSET;
                }

                cPit.ScreenLoc = new Point(posX, posY);
                cPit.Draw(gr);
            }
        }

        /* Reset the playing board */
        public void ResetBoard()
        {
            // Home pits cleared
            this.pits[0].RemoveStones();
            this.pits[PitsTotal - 1].RemoveStones();

            // Each pit cleared and stones added
            for (int i = 0; i < this.PlayPitsTotal; i++)
            {
                this.pits[i + 1].RemoveStones();
                this.pits[i + 1].Fill(this.startingStonesAmount);
            }
        }

        /* Checks if a row of play pits is empty for a specific player */
        public bool IsEmptyRow(Player player)
        {
            switch (player.ToString())
            {
                // Check for P2
                case "P2":
                {
                    for (int i = 1; i <= this.playPitsPerRow; i++)
                    {
                        if (this.pits[i].GetStones() != 0)
                        {
                            return false;
                        }
                    }

                    break;
                }
                // Check for P1
                case "P1":
                {
                    for (int i = this.playPitsPerRow + 1; i <= this.HomePitRight.IndexInList - 1; i++)
                    {
                        if (this.pits[i].GetStones() != 0)
                        {
                            return false;
                        }
                    }

                    break;
                }
            }

            return true;
        }

        /* Get the opposing pit of a certain pit on the board */
        public Pit OpposingPit(Pit cPit)
        {
            int indexOpposingPit;

            if (cPit.IndexInList < this.playPitsPerRow)
            {
                indexOpposingPit = cPit.IndexInList + this.playPitsPerRow;
            }
            else
            {
                indexOpposingPit = cPit.IndexInList - this.playPitsPerRow;
            }

            return this.pits[indexOpposingPit];
        }

        /* Transfer all stones within a row of play pits to linked home pit */
        public void TransferToHomePit(ITurn turnStrategy, Player receiver)
        {
            foreach (Pit sPit in this.pits)
            {
                if (turnStrategy.PitOwnedByPlayer(this, receiver, sPit))
                {
                    receiver.HomePit.Fill(sPit.StonesAmount);
                    sPit.RemoveStones();
                }
            }
        }

        /* Return the pit at a certain index in the board */
        public Pit GetPit(int index)
        {
            return this.pits[index];
        }


        /* Clone this instance of Board */
        public Board Clone()
        {
            Board cBoard = new Board(this.PlaysPitPerRow, this.startingStonesAmount)
            {
                // Init board size
                playPitsPerRow = this.playPitsPerRow,
                // Arrange array of pits
                startingStonesAmount = startingStonesAmount,
                pits = new Pit[(PlaysPitPerRow * 2) + 2]
            };

            // Clone all pits in board
            for (int i = 0; i < this.PitsTotal; i++)
            {
                cBoard.pits[i] = this.pits[i].Clone();
            }

            // Init pit sizes
            cBoard.homePitWidth = pits[0].Width;
            cBoard.homePitHeight = pits[0].Height;
            cBoard.playPitWidth = pits[1].Width;
            cBoard.playPitHeight = pits[1].Height;

            // Set dimensions of board 
            cBoard.width = (pits[0].Width * 2) + (pits[0].Width * cBoard.PlaysPitPerRow) + (PIT_OFFSET * (this.PlaysPitPerRow + 1)) + (2 * OFFSET_TO_BORDER);
            cBoard.height = (pits[0].Height) + this.PlaysPitPerRow + OFFSET_TO_BORDER * 2;

            // Set coordinates of board
            cBoard.coords.X = (800 - cBoard.width) / 2;
            cBoard.coords.Y = (600 - cBoard.height) / 2;

            // Set home pit owners
            cBoard.HomePitLeft.Owner = this.HomePitLeft.Owner;
            cBoard.HomePitRight.Owner = this.HomePitRight.Owner;

            return cBoard;
        }
    }
}
