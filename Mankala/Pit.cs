using System;
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

        protected int width;
        protected int height;
        
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

        public void RemoveStone()
        {
            this.stones.Clear();
        }

        public int GetStone()
        {
            return this.stones.Count;
            this.RemoveStone();
        }
    }
}
