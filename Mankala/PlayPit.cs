using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Mankala
{
    public class PlayPit : Pit
    {
        public PlayPit()
        {
            this.width = 80;
            this.height = 80;
        }

        public void RemoveStone()
        {
            this.stones.Clear();
        }
    }
}
