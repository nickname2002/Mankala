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
            return new Board(6, 5);
        }

        public IScore CreateScore()
        {
            return new CanaryScore();
        }

        public ITurn CreateTurn()
        {
            return new CanaryTurn();
        }

        public Player CreatePlayer(Board board, PlayerID id)
        {
            Player playerToReturn;

            if (id == PlayerID.P1)
            {
                playerToReturn = new Player("P1", board.HomePitLeft, board.HomePitRight);
                board.HomePitLeft.Owner = playerToReturn;
                return playerToReturn;
            }

            playerToReturn = new Player("P2", board.HomePitRight, board.HomePitLeft);
            board.HomePitRight.Owner = playerToReturn;
            return playerToReturn;
        }
    }
}
