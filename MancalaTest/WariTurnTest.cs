using Xunit;
using Mancala;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace MancalaTest
{
    public class WariTurnTest
    {
        [Theory]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 2, 1)]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 0, 0, 0, 0, 0 }, 0, 6, 7)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 10, 5)]
        public void TestNextPit(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int startingIndex, int expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new WariTurn();

            //Act
            Pit sPit = b.GetPit(startingIndex);
            int actualResult = turnStrategy.NextPit(b,sPit).IndexInList;

            //Assert
            Assert.Equal(expected, actualResult); 
        }

        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, 7, false)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 2, true)]
        public void TestPitOwnedByPlayerLeft(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int index, bool expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new WariTurn();

            //Act
            bool actual = turnStrategy.PitOwnedByPlayer(b, b.HomePitLeft.Owner, b.GetPit(index));

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, 7, true)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 2, false)]
        public void TestPitOwnedByPlayerRight(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int index, bool expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new WariTurn();

            //Act
            bool actual = turnStrategy.PitOwnedByPlayer(b, b.HomePitRight.Owner, b.GetPit(index));

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 1, 2)]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 0, 0, 0, 0, 0 }, 0, 6, 1)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 5, 10)]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 0, 0, 0, 0, 0 }, 0, 4, 5)]
        public void TestPrevious(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int startingIndex, int expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            WariTurn turnStrategy = new WariTurn();

            //Act
            Pit sPit = b.GetPit(startingIndex);
            int actualResult = turnStrategy.PreviousPit(b, sPit).IndexInList; 

            //Assert
            Assert.Equal(expected, actualResult);
        }



        private static Board CreateFakeBoard(int leftHomePit, int[] upper, int[] bottom, int rightHomePit)
        {
            WariFactory factory = new WariFactory();

            Board b = new Board(upper.Length, 5);
            Player p1 = factory.CreatePlayer(b, PlayerID.P1);
            Player p2 = factory.CreatePlayer(b, PlayerID.P2);

            // Create left home pit
            b.HomePitLeft.RemoveStones();
            b.HomePitLeft.Fill(leftHomePit);

            // Create upper row
            for (int i = 0; i < upper.Length; i++)
            {
                Pit cPit = b.GetPit(i + 1);
                cPit.RemoveStones();
                cPit.Fill(upper[i]);
            }

            // Create bottom row
            for (int i = 0; i < bottom.Length; i++)
            {
                Pit cPit = b.GetPit(i + upper.Length);
                cPit.RemoveStones();
                cPit.Fill(bottom[i]);
            }

            // Create right home pit
            b.HomePitRight.RemoveStones();
            b.HomePitRight.Fill(rightHomePit);

            return b;
        }
    }
}
