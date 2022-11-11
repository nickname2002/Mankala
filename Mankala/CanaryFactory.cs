using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class CanaryFactory : IMancalaFactory
    {
        
        public Board CreateBoard()
        {
            return new Board(8, 5); 
        }

        public IScore CreateScore()
        {
            throw new NotImplementedException();
        }

        public ITurn CreateTurn()
        {
            throw new NotImplementedException();
        }
    }
}
