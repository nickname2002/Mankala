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
        public bool PitOwnedByPlayer(Board board, Player cPlayer, Pit cPit);
        public Pit PerformTurn(Board board, Player cPlayer, Pit startingPit);
        public bool MovePossible(Board board, Player cPlayer);
        public void CaptureSeeds(Board board, Player cPlayer, Pit cPit);
    }
}
