using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mankala
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

        // Homepits
        public HomePit HomePitLeft
        {
            get
            {
                return (HomePit)pits[0];
            }
        }

        public HomePit HomePitRight
        {
            get
            {
                return (HomePit)pits[this.PitsTotal - 1];
            }
        }

        // Board dimensions
        public int PlaysPitPerRow 
        { 
            get
            {
                return playPitsPerRow;
            }
        }

        public int PlayPitsTotal 
        { 
            get
            {
                return PlaysPitPerRow * 2;
            }
        }

        public int PitsTotal 
        {   
            get
            {
                return PlaysPitPerRow * 2 + 2;
            } 
        }

        public int TotalStonesAmount
        {
            get
            {
                return startingStonesAmount * this.PlayPitsTotal;
            }
        }

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
            foreach (Pit pit in this.pits)
            {
                if (pit.Clicked(mouseLoc))
                {
                    return pit;
                }
            }

            return null;
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
            // Homepits cleared
            this.pits[0].RemoveStone();
            this.pits[PitsTotal - 1].RemoveStone();

            // Each pit cleared and stones added
            for (int i = 0; i < this.PlayPitsTotal; i++)
            {
                this.pits[i + 1].RemoveStone();
                this.pits[i + 1].Fill(this.startingStonesAmount);
            }
        }

        /* Get the next pit in counterclockwise direction */
        public Pit NextPit(Pit cPit)
        {
            // Left home pit
            if (cPit.IndexInList == HomePitLeft.IndexInList)
            {
                return this.pits[playPitsPerRow + 1]; 
            }

            // Right home pit
            if (cPit.IndexInList == HomePitRight.IndexInList)
            {
                return this.pits[playPitsPerRow];
            }

            // Top play pits
            if (cPit.IndexInList <= this.PlaysPitPerRow)
            {
                return this.pits[cPit.IndexInList - 1];
            }

            // Bottom play pits
            return this.pits[cPit.IndexInList + 1];
        }
    }
}
