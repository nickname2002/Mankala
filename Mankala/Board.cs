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

        // Homepits
        public Pit HomePitLeft
        {
            get
            {
                return pits[0];
            }
        }

        public Pit HomePitRight
        {
            get
            {
                return pits[this.PitsTotal - 1];
            }
        }

        // Pit dimensions
        private int homePitWidth;
        private int homePitHeight;
        private int playPitWidth;
        private int playPitHeight;

        // Starting number of stones per pit
        private int startingStonesAmount;
        private int playPitsPerRow;

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
        
        private Point coords;
        private int width;
        private int height;

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
            for (int i = 0; i < this.PitsTotal; i++)
            {
                if (i == 0 || i == PlaysPitPerRow)
                {
                    this.pits[i] = new HomePit(i);
                }
                else
                {
                    this.pits[i] = new PlayPit(i);
                }
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

            // Draw left home pit
            HomePitLeft.ScreenLoc = new Point(OFFSET_TO_BORDER + coords.X, coords.Y);
            gr.FillEllipse(Brushes.Sienna, HomePitLeft.ScreenLoc.X, HomePitLeft.ScreenLoc.Y, homePitWidth, homePitHeight);
            gr.DrawString((pits[0].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, HomePitLeft.ScreenLoc);

            // Draw right home pit
            HomePitRight.ScreenLoc = new Point(OFFSET_TO_BORDER + coords.X + PIT_OFFSET + homePitWidth + homePitWidth * PlaysPitPerRow + PIT_OFFSET * PlaysPitPerRow, coords.Y);
            gr.FillEllipse(Brushes.Sienna, HomePitRight.ScreenLoc.X, HomePitRight.ScreenLoc.Y, homePitWidth, homePitHeight);
            gr.DrawString((pits[this.PitsTotal - 1].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, HomePitRight.ScreenLoc);
        }

        /* Drawing play pits */
        private void DrawPlayPits(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw first row
            for (int i = 0; i < PlaysPitPerRow; i++)
            {
                pits[i + 1].ScreenLoc = new Point(OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y);
                gr.FillEllipse(Brushes.Sienna, pits[i + 1].ScreenLoc.X, pits[i + 1].ScreenLoc.Y, playPitWidth, playPitHeight);
                gr.DrawString((pits[i + 1].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, pits[i + 1].ScreenLoc.X, pits[i + 1].ScreenLoc.Y);
            }

            // Draw second row
            for (int i = 0; i < PlaysPitPerRow; i++)
            {
                pits[PlaysPitPerRow + i].ScreenLoc = new Point(OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y + playPitWidth + PIT_OFFSET);
                gr.FillEllipse(Brushes.Sienna, pits[PlaysPitPerRow + i].ScreenLoc.X, pits[PlaysPitPerRow + i].ScreenLoc.Y, playPitWidth, playPitHeight);
                gr.DrawString((pits[PlaysPitPerRow + i].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, pits[PlaysPitPerRow + i].ScreenLoc);
            }
        }

        /* Reset the playing board */
        public void ResetBoard()
        {
            // Homepits cleared
            this.pits[0].RemoveStone();
            this.pits[(PitsTotal - 1)].RemoveStone();

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
            // Index of the current pits
            int indexOfPit = cPit.IndexInList;

            // Index equal to right homepit
            if (indexOfPit == PitsTotal - 1)
            {
                return this.pits[this.PlaysPitPerRow];
            } 
            // Index equal to left homepit
            else if (indexOfPit == 0)
            {
                return this.pits[this.playPitsPerRow + 1];
            }
            // Index equal to playpit
            else
            {
                // Determine whether to go left or right
                if (indexOfPit <= playPitsPerRow)
                {
                    return this.pits[indexOfPit - 1];
                }
                else
                {
                    return this.pits[indexOfPit + 1];
                }
            }
        }
    }
}
