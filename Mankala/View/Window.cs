﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mancala
{
    enum GameState { Menu, Playing }

    public class Window : Form
    {
        // Screen dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        // Mancala game
        MancalaGame game;
        GameState state = GameState.Menu;

        // Game lables
        Button mancalaButton;
        Button wariButton;
        Button canaryButton;

        // Constructor 
        public Window()
        {
            // Window properties
            this.DoubleBuffered = true;
            this.ClientSize = new Size(WIDTH, HEIGHT);
            this.BackColor = Color.FromArgb(255, 240, 210);
            this.Text = "Mancala";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.KeyPreview = true;

            // Initialize Mancala play buttons
            mancalaButton = new Button
            {
                Text = "Mancala Traditional",
                Location = new Point(this.Width / 2 - 100, 350),
                Size = new Size(200, 200),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Bodoni MT", 20),
                ForeColor = Color.Gold,
                BackColor = Color.Sienna
            };

            // Initialize Wari play buttons
            wariButton = new Button
            {
                Text = "Wari",
                Location = new Point((this.Width / 2) / 2 - 100, 350),
                Size = new Size(200, 200),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Bodoni MT", 20),
                ForeColor = Color.Gold,
                BackColor = Color.Sienna
            };

            // Initialize Canary play buttons
            canaryButton = new Button
            {
                Text = "Canary",
                Location = new Point((this.Width / 2) + 103, 350),
                Size = new Size(200, 200),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Bodoni MT", 20),
                ForeColor = Color.Gold,
                BackColor = Color.Sienna
            };

            // Button event handlers
            mancalaButton.Click += MancalaSelect;
            wariButton.Click += WariSelect;
            canaryButton.Click += CanarySelect;

            // Add labels to controls
            this.Controls.Add(mancalaButton);
            this.Controls.Add(wariButton);
            this.Controls.Add(canaryButton);

            // Window event handlers
            this.Paint += Draw;
            this.MouseClick += ClickScreen;
            this.KeyPress += KeyEventHandler;
        }

        /* Draw event handler */
        private void Draw(object sender, PaintEventArgs pea)
        {
            switch (this.state)
            {
                case GameState.Menu:
                    DrawMenu(pea.Graphics);
                    break;

                case GameState.Playing:
                    game.DrawScore(pea.Graphics);
                    game.board.Draw(pea.Graphics);
                    break;

                default:
                    break;
            }
        }

        /* Draw the game menu */
        public void DrawMenu(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Draw logo
            Point logoPos = new Point(50, 40);
            gr.FillEllipse(Brushes.Sienna, logoPos.X, logoPos.Y, 700, 200);
            gr.DrawEllipse(new Pen(Brushes.Chocolate, 10), logoPos.X, logoPos.Y, 700, 200);
            gr.DrawString("Mancala", new Font("Bodoni MT", 80), Brushes.Gold, new Point(logoPos.X + 135, logoPos.Y + 35));

            // Draw names
            gr.DrawString("Isabelle de Wolf & Nick Jordan", new Font("Bodoni MT", 20), Brushes.Chocolate, new Point(logoPos.X + 175, logoPos.Y + 220));

            // Handle visibility of all buttons
            TogglePlayButtons();
        }

        /* Keyboard event handling */
        private void KeyEventHandler(object sender, KeyPressEventArgs e)
        {
            if (this.game == null)
            {
                return;
            }

            if (this.game.GameOver() && e.KeyChar == 'c')
            {
                this.state = GameState.Menu;
            }

            this.Invalidate();
        }

        /* Click event handlers for the game option buttons */

        private void MancalaSelect(object sender, EventArgs ea)
        {
            this.game = new MancalaGame(new MancalaFactory());
            this.state = GameState.Playing;

            this.TogglePlayButtons();
            this.Invalidate();
        }

        private void WariSelect(object sender, EventArgs ea)
        {
            this.game = new MancalaGame(new WariFactory());
            this.state = GameState.Playing;

            this.TogglePlayButtons(); 
            this.Invalidate(); 
        }

        private void CanarySelect(object sender, EventArgs ea)
        {
            this.game = new MancalaGame(new CanaryFactory());
            this.state = GameState.Playing;

            this.TogglePlayButtons();
            this.Invalidate();
        }

        /* Screen click event handler */
        private void ClickScreen(object sender, MouseEventArgs mea)
        {
            if (this.state == GameState.Playing)
            {
                Console.WriteLine(game.board.ClickPit(new Point(347, 315)));
                game.PerformTurn(mea.Location);
                this.Invalidate();
            }
        }

        /* Toggle play buttons */
        private void TogglePlayButtons()
        {
            this.mancalaButton.Visible = !this.mancalaButton.Visible;
            this.wariButton.Visible = !this.wariButton.Visible;
            this.canaryButton.Visible = !this.canaryButton.Visible;
        }
    }
}
