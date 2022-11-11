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

        [Theory]
        [InlineData(0, new int[] { 1, 1, 1, 0, 1 }, new int[] { 5, 5, 5, 5, 5 }, 0, true)]
        public void TestIsValidTurnLeft(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, bool expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            WariTurn turnStrategy = new WariTurn();

            //Act
            bool actual = turnStrategy.IsValidTurn(b, b.HomePitLeft.Owner);

            //Assert
            Assert.Equal(expected, actual); 
        }


        [Theory]
        [InlineData(0, new int[] { 1, 1, 1, 0, 1 }, new int[] { 5, 5, 5, 5, 5 }, 0, true)]
        public void TestIsValidTurnRight(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, bool expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            WariTurn turnStrategy = new WariTurn();

            //Act
            bool actual = turnStrategy.IsValidTurn(b, b.HomePitRight.Owner);

            //Assert
            Assert.Equal(expected, actual);
        }

       
        [Theory]
        [InlineData(0, new int[] { 7, 7, 2, 2, 4 }, new int[] { 4, 3, 0, 1, 2}, 2, 3, 6 )]
        [InlineData(0, new int[] { 7, 7, 2, 2, 4 }, new int[] { 4, 3, 0, 1, 2 }, 0, 3, 4)]
        [InlineData(0, new int[] { 1, 1, 1, 0, 1 }, new int[] { 4, 3, 0, 1, 2 }, 0, 3, 0)]
        private void CaptureSeedsRightPlayer(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int pitIndex, int expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new WariTurn();
            Pit cPit = b.GetPit(pitIndex);

            // Act
            turnStrategy.CaptureSeeds(b, b.HomePitRight.Owner, cPit);
            int actualResult = b.HomePitRight.StonesAmount;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData(0, new int[] { 7, 7, 2, 2, 4 }, new int[] { 4, 3, 2, 5, 5 }, 0, 7, 5)]
        [InlineData(7, new int[] { 7, 7, 2, 2, 4 }, new int[] { 4, 3, 2, 5, 5 }, 0, 7, 12)]
        [InlineData(2, new int[] { 1, 1, 1, 0, 1 }, new int[] { 4, 3, 0, 1, 2 }, 0, 10, 2)]
        private void CaptureSeedsLeftPlayer(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int pitIndex, int expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new WariTurn();
            Pit cPit = b.GetPit(pitIndex);

            // Act
            turnStrategy.CaptureSeeds(b, b.HomePitLeft.Owner, cPit);
            int actualResult = b.HomePitLeft.StonesAmount;

            // Assert
            Assert.Equal(expectedResult, actualResult);
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
