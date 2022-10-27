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
        Pit[] pits;

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
                    this.pits[i] = new HomePit();
                }
                else
                {
                    this.pits[i] = new PlayPit();
                }
            }

            // Add stones to the pits
            ResetBoard();
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
            // Color: Brushes.Peru
            gr.FillRectangle(Brushes.Peru, coords.X, coords.Y - OFFSET_TO_BORDER, this.width, this.height);
        }

        /* Drawing home pits */
        private void DrawHomePits(Graphics gr)
        {
            // Draw left home pit
            gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X, coords.Y, homePitWidth, homePitHeight);
            gr.DrawString((pits[0].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, OFFSET_TO_BORDER + coords.X, coords.Y);

            // Draw right home pit
            gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X + PIT_OFFSET + homePitWidth + homePitWidth * PlaysPitPerRow + PIT_OFFSET * PlaysPitPerRow, coords.Y, homePitWidth, homePitHeight);
            gr.DrawString((pits[this.PitsTotal - 1].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, OFFSET_TO_BORDER + coords.X + PIT_OFFSET + homePitWidth + homePitWidth * PlaysPitPerRow + PIT_OFFSET * PlaysPitPerRow, coords.Y);
        }

        /* Drawing play pits */
        private void DrawPlayPits(Graphics gr)
        {
            // Draw first row
            for (int i = 0; i < PlaysPitPerRow; i++)
            {
                gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y, playPitWidth, playPitHeight);
                gr.DrawString((pits[i + 1].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y);
            }

            // Draw second row
            for (int i = 0; i < PlaysPitPerRow; i++)
            {
                gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y + playPitWidth + PIT_OFFSET, playPitWidth, playPitHeight);
                gr.DrawString((pits[PlaysPitPerRow + i].StonesAmount).ToString(), new Font("Arial", 16), Brushes.Gold, OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y + playPitWidth + PIT_OFFSET);
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
    }
}
