using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class WariFactory : IMancalaFactory
    {
        public Board CreateBoard()
        {
            return new Board(6, 4);
        }

        public IScore CreateScore()
        {
            return new WariScore();
        }

        public ITurn CreateTurn()
        {
            return new WariTurn();
        }
    }
}
