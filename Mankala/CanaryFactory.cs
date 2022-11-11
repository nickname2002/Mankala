using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class CanaryFactory : IMancalaFactory
    {
        public CanaryFactory()
        {

        }
        public Board CreateBoard()
        {
            throw new NotImplementedException();
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
