using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mankala
{
    public class Window : Form
    {
        // Screen dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        // Mancala game
        MancalaGame game;

        // Constructor 
        public Window()
        {
            this.ClientSize = new Size(WIDTH, HEIGHT);
            this.Text = "Mankala";
            this.game = new MancalaGame();
            this.Paint += Draw;
        }

        // Draw event handler
        private void Draw(object sender, PaintEventArgs pea)
        {
            game.board.Draw(pea.Graphics);
        }
    }
}
