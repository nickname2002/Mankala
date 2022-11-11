using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
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

        public Player CreatePlayer(Board board, PlayerID id)
        {
            Player playerToReturn;

            if (id == PlayerID.P1)
            {
                playerToReturn = new Player("P1", board.HomePitRight, board.HomePitLeft);
                board.HomePitRight.Owner = playerToReturn;
                return playerToReturn;
            }

            playerToReturn = new Player("P2", board.HomePitLeft, board.HomePitRight);
            board.HomePitLeft.Owner = playerToReturn;
            return playerToReturn;
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
