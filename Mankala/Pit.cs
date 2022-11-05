﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mankala
{
    public abstract class Pit
    {
        protected List<Stone> stones;
        public int StonesAmount
        {
            get
            {
                return stones.Count;
            }
        }

        protected int index;
        public int IndexInList
        {
            get
            {
                return index;
            }
        }

        protected Point screenLoc;
        protected int width;
        protected int height;

        public Point ScreenLoc
        {
            get
            {
                return screenLoc;
            }

            set
            {
                this.screenLoc = value;
            }
        }

        public int Width 
        {
            get 
            {
                return width;
            } 
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public Pit()
        {

        }

        public void Draw(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gr.FillEllipse(Brushes.Sienna, ScreenLoc.X, ScreenLoc.Y, width, height);
            gr.DrawString(StonesAmount.ToString(), new Font("Trebuchet MS", 20), Brushes.Gold, ScreenLoc.X + (width / 3f), ScreenLoc.Y + (height / 3f));
        }

        public void Fill(int stonesAmount)
        {
            for (int i = 0; i < stonesAmount; i++)
            {
                AddStone();
            }
        }

        public void AddStone()
        {
            this.stones.Add(new Stone());
        }

        public void RemoveStones()
        {
            this.stones.Clear();
        }

        public int GetStones()
        {
            return this.stones.Count;
        }

        public bool IsEmpty()
        {
            return this.StonesAmount == 0;
        }

        public bool Clicked(Point mouseLoc)
        {
            bool withinRangeX = mouseLoc.X > screenLoc.X && mouseLoc.X < screenLoc.X + this.width;
            bool withinRangeY = mouseLoc.Y > screenLoc.Y && mouseLoc.Y < screenLoc.Y + this.height;

            return withinRangeX && withinRangeY;
        }

        public override string ToString()
        {
            return this.GetType().ToString().Split('.')[1];
        }

        public abstract Pit Clone();
    }
}
