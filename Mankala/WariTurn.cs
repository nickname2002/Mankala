using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public class WariTurn : ITurn
    {
        public void OnEmptyFriendlyAction(Board board, Player cPlayer, Pit cPit)
        {
            throw new NotImplementedException();
        }

        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit)
        {
            throw new NotImplementedException();
        }
    }
}
