using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mankala
{
    public class HomePit : Pit
    {
        public HomePit(int indexInList) 
        {
            this.width = 80;
            this.height = 170;
            this.stones = new List<Stone>();
            this.index = indexInList;
        }
    }
}
