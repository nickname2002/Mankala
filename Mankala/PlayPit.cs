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
        public PlayPit(int indexInList)
        {
            this.width = 80;
            this.height = 80;
            this.stones = new List<Stone>();
            this.index = indexInList;
        }

        public override Pit Clone()
        {
            PlayPit clPit = new PlayPit(this.IndexInList);
            clPit.width = this.width;
            clPit.height = this.height;
            clPit.Fill(this.StonesAmount);
            return clPit;
        }
    }
}
