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
        public int StonesAmount { get; set; }

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

        public void AddStone()
        {
            this.stones.Add(new Stone());
        }
    }
}
