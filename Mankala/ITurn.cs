using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mankala
{
    public interface ITurn
    {
        public Pit NextPit(Board board, Pit cPit);
        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit);
        public void CaptureSeeds(Board board, Player cPlayer, Pit cPit);
    }
}
