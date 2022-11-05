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
        private Player? owner;

        public Player Owner
        {
            get 
            { 
                return owner; 
            }
            set
            {
                if (owner == null)
                {
                    this.owner = value;
                }
            }
        }

        public HomePit(int indexInList) 
        {
            this.width = 80;
            this.height = 170;
            this.stones = new List<Stone>();
            this.index = indexInList;
        }

        public override Pit Clone()
        {
            HomePit clPit = new HomePit(this.IndexInList);
            clPit.width = this.width;
            clPit.height = this.height;
            clPit.Fill(this.StonesAmount);
            return clPit;
        }
    }
}
