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

        // Board dimensions
        public int Size { get; set; }
        private Point coords;
        private int width;
        private int height;

        public Board(int size)
        {
            // Init board size
            this.Size = size;

            // Arrange array of pits
            this.pits = new Pit[Size + 2];
            this.FillBoard();

            // Init pit sizes
            this.homePitWidth = pits[0].Width;
            this.homePitHeight = pits[0].Height;
            this.playPitWidth = pits[1].Width;
            this.playPitHeight = pits[1].Height;

            // Set dimensions of board 
            this.width = (pits[0].Width * 2) + (pits[0].Width * this.Size) + (PIT_OFFSET * (this.Size + 1)) + (2 * OFFSET_TO_BORDER);
            this.height = (pits[0].Height) + this.Size + OFFSET_TO_BORDER * 2;

            // Set coordinates of board
            this.coords.X = (800 - this.width) / 2;
            this.coords.Y = (600 - this.height) / 2;
        }

        /* Fill board with pits */
        private void FillBoard()
        {
            for (int i = 0; i < (Size + 1); i++)
            {
                if (i == 0 || i == Size)
                {
                    this.pits[i] = new HomePit();
                }
                else
                {
                    this.pits[i] = new PlayPit();
                }
            }
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

            // Draw right home pit
            gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X + PIT_OFFSET + homePitWidth + homePitWidth * Size + PIT_OFFSET * Size, coords.Y, homePitWidth, homePitHeight);
        }

        /* Drawing play pits */
        private void DrawPlayPits(Graphics gr)
        {
            // Draw first row
            for (int i = 0; i < Size; i++)
            {
                gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y, playPitWidth, playPitHeight);
            }

            // Draw secod row
            for (int i = 0; i < Size; i++)
            {
                gr.FillEllipse(Brushes.Sienna, OFFSET_TO_BORDER + coords.X + playPitWidth + PIT_OFFSET + playPitWidth * i + PIT_OFFSET * i, coords.Y + playPitWidth + PIT_OFFSET, playPitWidth, playPitHeight);
            }
        }
    }
}
