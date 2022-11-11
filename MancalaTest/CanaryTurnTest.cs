using Xunit;
using Mancala;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace MancalaTest
{
    public class CanaryTurnTest
    {
        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 0, 0, 0, 0, 0 }, 0, 0, 1)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 2, 3)]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 0, 0, 0, 0, 0 }, 0, 6, 0)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 11, 10)]
        public void TestNextPit(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int startingIndex, int expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new CanaryTurn();

            // Act
            Pit sPit = b.GetPit(startingIndex);
            int actualResult = turnStrategy.NextPit(b, sPit).IndexInList;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(20, new int[] { 0, 0, 0, 2, 0 }, new int[] { 2, 3, 4, 2, 1 }, 16, 4, 20)]
        private void TestCaptureSeedsLeftPlayer(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int pitIndex, int expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new CanaryTurn();
            Pit cPit = b.GetPit(pitIndex);

            // Act
            turnStrategy.CaptureSeeds(b, b.HomePitLeft.Owner, cPit);
            int actualResult = b.HomePitLeft.StonesAmount;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(20, new int[] { 0, 0, 0, 2, 0 }, new int[] { 2, 3, 4, 2, 1 }, 16, 10, 16)]
        private void TestCaptureSeedsRightPlayer(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int pitIndex, int expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new CanaryTurn();
            Pit cPit = b.GetPit(pitIndex);

            // Act
            turnStrategy.CaptureSeeds(b, b.HomePitRight.Owner, cPit);
            int actualResult = b.HomePitRight.StonesAmount;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, 7, true)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 2, false)]
        public void TestPitOwnedByPlayerLeft(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int index, bool expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new CanaryTurn();

            //Act
            bool actual = turnStrategy.PitOwnedByPlayer(b, b.HomePitLeft.Owner, b.GetPit(index));

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, 7, false)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, 2, true)]
        public void TestPitOwnedByPlayerRight(int leftHomePit, int[] upper, int[] bottom, int rightHomePit, int index, bool expected)
        {
            //Arrange 
            Board b = CreateFakeBoard(leftHomePit, upper, bottom, rightHomePit);
            ITurn turnStrategy = new CanaryTurn();

            //Act
            bool actual = turnStrategy.PitOwnedByPlayer(b, b.HomePitRight.Owner, b.GetPit(index));

            //Assert
            Assert.Equal(expected, actual);
        }

        private static Board CreateFakeBoard(int leftHomePit, int[] upper, int[] bottom, int rightHomePit)
        {
            MancalaFactory factory = new MancalaFactory();

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
