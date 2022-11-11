using Xunit;
using Mancala;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace MancalaTest
{
    public class MancalaScoreTest
    {
        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, false)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, true)]
        public void GameOverTest(int homePitLeft, int[] upper, int[] bottom, int homePitRight, bool expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(homePitLeft, upper, bottom, homePitRight);
            IScore scoreStrategy = new MancalaScore();

            //Act 
            bool actualResult = scoreStrategy.GameOver(new MancalaTurn(), b);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, false)]
        [InlineData(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, true)]
        public void EmptyRow(int homePitLeft, int[] upper, int[] bottom, int homePitRight, bool expectedResult)
        {
            // Arrange
            Board b = CreateFakeBoard(homePitLeft, upper, bottom, homePitRight);
            IScore scoreStrategy = new MancalaScore();

            //Act 
            Player cPlayer = b.HomePitLeft.Owner;
            scoreStrategy.CheckForEmptyRow(new MancalaTurn(), b, cPlayer);
            bool actualResult = true;
            List<Pit> cPlayerPits = b.GetPlayPits(cPlayer);

            foreach (Pit p in cPlayerPits)
            {
                if (p.StonesAmount != 0)
                {
                    actualResult = false;
                    break;
                }
            }

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [Theory]
        [InlineData(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0, false)]
        [InlineData(26, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 25, true)]
        public void TestWinningStonesAmountReached(int homePitLeft, int[] upper, int[] bottom, int homePitRight, bool expectedResult)
        {
            //Arrange 
            Board b = CreateFakeBoard(homePitLeft, upper, bottom, homePitRight);
            IScore scoreStrategy = new MancalaScore();

            //Act
            bool actualResult = scoreStrategy.WinningStonesAmountReached(b);

            //Assert 
            Assert.Equal(expectedResult, actualResult); 
        }

        [Fact]
        public void TestDraw()
        {
            // Arrange
            Board b = CreateFakeBoard(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 }, 25);
            IScore scoreStrategy = new MancalaScore();
            bool expectedResult = true;

            // Act
            bool actualResult = scoreStrategy.IsDraw(b, b.HomePitLeft, b.HomePitRight);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestDrawFail()
        {
            // Arrange
            Board b = CreateFakeBoard(30, new int[] { 1, 0, 1, 1, 1 }, new int[] { 1, 1, 1, 0, 1 }, 12);
            IScore scoreStrategy = new MancalaScore();
            bool expectedResult = false;

            // Act
            bool actualResult = scoreStrategy.IsDraw(b, b.HomePitLeft, b.HomePitRight);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestIsOnlyWinner()
        {
            // Arrange
            Board b = CreateFakeBoard(30, new int[] { 1, 0, 1, 1, 1 }, new int[] { 1, 1, 1, 0, 1 }, 12);
            IScore scoreStrategy = new MancalaScore();
            bool expectedResult = true;

            // Act
            bool actualResult = scoreStrategy.IsOnlyWinner(b.HomePitLeft.Owner);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestIsOnlyWinnerFail()
        {
            // Arrange
            Board b = CreateFakeBoard(25, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0 }, 25);
            IScore scoreStrategy = new MancalaScore();
            bool expectedResult = false;

            // Act
            bool actualResult = scoreStrategy.IsOnlyWinner(b.HomePitRight.Owner);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestGetWinner()
        {
            // Arrange
            Board b = CreateFakeBoard(26, new int[] { 0, 0, 0, 0, 0 }, new int[] { 0, 1, 1, 0, 1 }, 24);
            IScore scoreStrategy = new MancalaScore();
            Player expectedResult = b.HomePitLeft.Owner;

            // Act
            Player? actualResult = scoreStrategy.GetWinner(b);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestGetWinnerFail()
        {
            // Arrange
            Board b = CreateFakeBoard(0, new int[] { 5, 5, 5, 5, 5 }, new int[] { 5, 5, 5, 5, 5 }, 0);
            IScore scoreStrategy = new MancalaScore();
            Player? expectedResult = null;

            // Act
            Player? actualResult = scoreStrategy.GetWinner(b);

            // Assert
            Assert.Equal(expectedResult, actualResult);
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
