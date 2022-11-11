using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Mankala
{
    public interface IMancalaFactory
    {
        public IScore CreateScore();
        public Board CreateBoard();   
        public ITurn CreateTurn();
        public Player CreatePlayer(Board board, PlayerID id);
    }
}
