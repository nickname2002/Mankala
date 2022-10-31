using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public interface ITurn
    {
        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit);
    }
}
