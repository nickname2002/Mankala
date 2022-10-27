using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class MancalaFactory : IMancalaFactory
    {
        public MancalaFactory()
        {

        }

        public Board CreateBoard()
        {
            return new Board(6, 4);
        }

        public IScore CreateScore()
        {
            return new MancalaScore();
        }

        public ITurn CreateTurn()
        {
            return new MancalaTurn();
        }
    }
}
