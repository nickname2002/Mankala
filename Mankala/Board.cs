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
        // Contains pits in board
        Pit[] pits;

        // Board properties
        private Point coords; 
        public int Size { get; set; }
        private int width;
        private int height;
        private int offsetToBorder = 20;
        private const int pitOffset = 10;

        public Board(int size)
        {
            this.Size = size;

            this.pits = new Pit[Size + 2];
            this.FillBoard();

            this.width = (pits[0].Width * 2) + (pits[0].Width * this.Size) + (pitOffset * (this.Size + 1)) + (2 * offsetToBorder);
            this.height = (pits[0].Height) + this.Size + offsetToBorder * 2;

            this.coords.X = (800 - this.width) / 2;
            this.coords.Y = 200;
        }

        public void Draw(Graphics gr)
        {
            DrawBoardBackdrop(gr);
            DrawHomePits(gr);
            DrawPlayPits(gr);
        }

        /* Fill board */
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

        /* Draw backdrop of the board */
        public void DrawBoardBackdrop(Graphics gr)
        {
            // Color: Brushes.Peru
            gr.FillRectangle(Brushes.Peru, coords.X, coords.Y - offsetToBorder, this.width, this.height);
        }

        /* Drawing play pits */
        private void DrawHomePits(Graphics gr)
        {
            // Pit dimensions
            int homeWidth = 80;
            int homeHeight = 170;

            // Draw first home pit
            gr.FillEllipse(Brushes.Sienna, offsetToBorder + coords.X, coords.Y, homeWidth, homeHeight);

            // Draw second home pit
            gr.FillEllipse(Brushes.Sienna, offsetToBorder + coords.X + pitOffset + homeWidth + homeWidth * Size + pitOffset * Size, coords.Y, homeWidth, homeHeight);
        }

        /* Drawing play pits */
        private void DrawPlayPits(Graphics gr)
        {
            // Pit dimensions
            int playWidth = 80;
            int playHeight = 80;

            // Draw play pits
            for (int i = 0; i < Size; i++)
            {
                gr.FillEllipse(Brushes.Sienna, offsetToBorder + coords.X + playWidth + pitOffset + playWidth * i + pitOffset * i, coords.Y, playWidth, playHeight);
            }

            for (int i = 0; i < Size; i++)
            {
                gr.FillEllipse(Brushes.Sienna, offsetToBorder + coords.X + playWidth + pitOffset + playWidth * i + pitOffset * i, coords.Y + playWidth + pitOffset, playWidth, playHeight);
            }
        }
    }
}
